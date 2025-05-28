using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace OgcApi.Net.SQLite;

public class SpatialSqliteConnection : SqliteConnection
{
    // Path to your SpatiaLite module (e.g., mod_spatialite.dll)
    // Make sure this DLL and its dependencies (GEOS, PROJ, etc.) are accessible.
    // A common approach is to place them in your application's output directory.
    private const string SpatiaLiteModulePath = "mod_spatialite"; // or "mod_spatialite.dll" on Windows etc.

    public SpatialSqliteConnection() : base() { }
    public SpatialSqliteConnection(string connectionString) : base(connectionString) { }

    public override void Open()
    {
        base.Open();

        InitializeSpatialDatabase();
    }
    public override async Task OpenAsync(CancellationToken cancellationToken)
    {
        await base.OpenAsync(cancellationToken);

        await InitializeSpatialDatabaseAsync(cancellationToken);
    }

    private static Lazy<object> initialize = new(__SetupPathVariable, isThreadSafe: true);

    private static object __SetupPathVariable()
    {
        string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        string path = Path.Combine(dir, "runtimes",
            Environment.Is64BitProcess ? "win-x64" : "win-x86", "native");

        Environment.SetEnvironmentVariable("PATH", path + ";" + Environment.GetEnvironmentVariable("PATH"));

        return null;
    }
    private static void InitSpatiaLite()
    {
        var _ = initialize.Value;
    }

    private void InitializeSpatialDatabase()
    {
        // enable extension loading - a crucial step for security reasons in SQLite.
        EnableExtensions(true);

        InitSpatiaLite();

        // Load the SpatiaLite extension
        // The filename should be just the name of the module (e.g., "mod_spatialite").
        // SQLite will automatically add the correct platform-specific extension (.dll, .so, .dylib).
        // Ensure the module is in a discoverable location (e.g., same directory as your app).
        LoadExtension(SpatiaLiteModulePath);

        // verify SpatiaLite is loaded by calling a SpatiaLite function
        using (var command = CreateCommand())
        {
            // initialize SpatiaLite metadata (if not already initialized, i.e. call only once per database)
            // this creates the core SpatiaLite system tables.
            command.CommandText = """
                SELECT InitSpatialMetaData()
                WHERE NOT EXISTS (SELECT * FROM sqlite_master WHERE type = 'table' AND name = 'spatial_ref_sys');
            """;
            command.ExecuteNonQuery();
        }
    }
    private async Task InitializeSpatialDatabaseAsync(CancellationToken token)
    {
        // enable extension loading - a crucial step for security reasons in SQLite.
        EnableExtensions(true);

        // Load the SpatiaLite extension
        // The filename should be just the name of the module (e.g., "mod_spatialite").
        // SQLite will automatically add the correct platform-specific extension (.dll, .so, .dylib).
        // Ensure the module is in a discoverable location (e.g., same directory as your app).
        LoadExtension(SpatiaLiteModulePath);

        // verify SpatiaLite is loaded by calling a SpatiaLite function
        using (var command = CreateCommand())
        {
            command.CommandText = "SELECT spatialite_version();";
            var spatialiteVersion = (await command.ExecuteScalarAsync(token))?.ToString();

            // initialize SpatiaLite metadata (if not already initialized, i.e. call only once per database)
            // this creates the core SpatiaLite system tables.
            command.CommandText = "SELECT InitSpatialMetaData();";
            await command.ExecuteScalarAsync(token);
        }
    }

    static SpatialSqliteConnection()
    {
        // ensure SQLitePCLRaw is initialized - this should be done once at application startup.
        SQLitePCL.Batteries.Init();
    }
}
