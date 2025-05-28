using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using OgcApi.Net.DataProviders;
using OgcApi.Net.Options;
using OgcApi.Net.Options.Features;
using System.Data.Common;

namespace OgcApi.Net.SQLite;

[OgcFeaturesProvider("SQLite", typeof(SqlFeaturesSourceOptions))]
[OgcTilesProvider("SQLite", null)]
public class SQLiteProvider(ILogger<SQLiteProvider> logger, IOptionsMonitor<OgcApiOptions> options)
    : SqlDataProvider(logger, options)
{
    protected override DbConnection GetDbConnection(string connectionString)
    {
        return new SpatialSqliteConnection(connectionString);
    }

    protected override IFeaturesSqlQueryBuilder GetFeaturesSqlQueryBuilder(SqlFeaturesSourceOptions collectionOptions)
    {
        return new FeaturesSqlQueryBuilder(collectionOptions);
    }

    protected override Geometry ReadGeometry(DbDataReader dataReader, int ordinal, SqlFeaturesSourceOptions collectionSourceOptions)
    {
        var geometryStream = dataReader.GetStream(ordinal);

        var geometryReader = new GaiaGeoReader();
        return geometryReader.Read(geometryStream);
    }
}