using Microsoft.Data.Sqlite;
using OgcApi.Net.SQLite;
using System;
using System.IO;
using System.Linq;

namespace OgcApi.SQLite.Tests.Utils;

public static class DatabaseUtils
{
    public const string DatabaseName = "OgcApiTests.db";

    private const string ConnectionStringTemplateEnvVariable = "CONNECTION_STRING_TEMPLATE";

    private const string DbConnectionString = "Data Source={0}";

    public static void RecreateDatabase()
    {
        if (File.Exists(DatabaseName))
            File.Delete(DatabaseName);

        using (var sqlConnection = new SpatialSqliteConnection(string.Format(GetConnectionStringTemplate(), DatabaseName)))
        {
            sqlConnection.Open();

            string script = string.Format(GetInstallSQLiteScript("DatabaseInstall"), DatabaseName);

            using var installDatabaseCommand = new SqliteCommand(script, sqlConnection);
            installDatabaseCommand.ExecuteNonQuery();
            // FIXME: this second command intentional
            // Geometry columns only work when table is created, dropped and created again
            // nasty hack needs to be fixed
            installDatabaseCommand.ExecuteNonQuery();
        }
    }

    private static string GetInstallSQLiteScript(string scriptName)
    {
        var assembly = typeof(DatabaseUtils).Assembly;
        using var stream = assembly.GetManifestResourceStream($"OgcApi.SQLite.Tests.Utils.{scriptName}.sql");

        if (stream == null)
        {
            throw new InvalidOperationException($"Database script is not found in the assembly `{assembly}`.");
        }

        using var streamReader = new StreamReader(stream);
        return streamReader.ReadToEnd();
    }

    private static string GetConnectionStringTemplate()
    {
        return Environment.GetEnvironmentVariable(ConnectionStringTemplateEnvVariable) ?? DbConnectionString;
    }

    public static string GetConnectionString()
    {
        return string.Format(GetConnectionStringTemplate(), DatabaseName);
    }
}