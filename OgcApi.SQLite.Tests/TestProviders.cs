using OgcApi.Net.Options.Features;
using OgcApi.Net.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OgcApi.SQLite.Tests.Utils;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using OgcApi.Net.SQLite;

namespace OgcApi.SQLite.Tests;

public static class TestProviders
{
    private static OgcApiOptions GetDefaultOptions()
    {
        return new OgcApiOptions
        {
            Collections = new CollectionsOptions
            {
                Items =
                [
                    new CollectionOptions
                    {
                        Id = "Polygons",
                        Features = new CollectionFeaturesOptions
                        {
                            Crs =
                            [
                                new Uri("http://www.opengis.net/def/crs/OGC/1.3/CRS84"),
                                new Uri("http://www.opengis.net/def/crs/EPSG/0/3857")
                            ],
                            StorageCrs = new Uri("http://www.opengis.net/def/crs/EPSG/0/3857"),
                            Storage = new SqlFeaturesSourceOptions
                            {
                                Type = "SQLite",
                                ConnectionString = DatabaseUtils.GetConnectionString(),
                                Schema = "main",
                                Table = "Polygons",
                                IdentifierColumn = "Id",
                                GeometryColumn = "Geom",
                                GeometryDataType = "geometry",
                                GeometrySrid = 3857,
                                DateTimeColumn = "Date",
                                Properties =
                                [
                                    "Name",
                                    "Number",
                                    "S",
                                    "Date"
                                ]
                            }
                        }
                    },

                    new CollectionOptions
                    {
                        Id = "LineStrings",
                        Features = new CollectionFeaturesOptions
                        {
                            Crs =
                            [
                                new Uri("http://www.opengis.net/def/crs/OGC/1.3/CRS84"),
                                new Uri("http://www.opengis.net/def/crs/EPSG/0/3857")
                            ],
                            StorageCrs = new Uri("http://www.opengis.net/def/crs/EPSG/0/3857"),
                            Storage = new SqlFeaturesSourceOptions
                            {
                                Type = "SQLite",
                                ConnectionString = DatabaseUtils.GetConnectionString(),
                                Schema = "main",
                                Table = "LineStrings",
                                IdentifierColumn = "Id",
                                GeometryColumn = "Geom",
                                GeometryDataType = "geometry",
                                GeometrySrid = 3857,
                                Properties = ["Name"]
                            }
                        }
                    },

                    new CollectionOptions
                    {
                        Id = "Points",
                        Features = new CollectionFeaturesOptions
                        {
                            Crs =
                            [
                                new Uri("http://www.opengis.net/def/crs/OGC/1.3/CRS84"),
                                new Uri("http://www.opengis.net/def/crs/EPSG/0/3857")
                            ],
                            StorageCrs = new Uri("http://www.opengis.net/def/crs/EPSG/0/3857"),
                            Storage = new SqlFeaturesSourceOptions
                            {
                                Type = "SQLite",
                                ConnectionString = DatabaseUtils.GetConnectionString(),
                                Schema = "main",
                                Table = "Points",
                                IdentifierColumn = "Id",
                                GeometryColumn = "Geom",
                                GeometryDataType = "geometry",
                                GeometrySrid = 3857,
                                Properties = ["Name"]
                            }
                        }
                    },

                    new CollectionOptions
                    {
                        Id = "Empty",
                        Features = new CollectionFeaturesOptions
                        {
                            Crs =
                            [
                                new Uri("http://www.opengis.net/def/crs/OGC/1.3/CRS84"),
                                new Uri("http://www.opengis.net/def/crs/EPSG/0/3857")
                            ],
                            StorageCrs = new Uri("http://www.opengis.net/def/crs/EPSG/0/3857"),
                            Storage = new SqlFeaturesSourceOptions
                            {
                                Type = "SQLite",
                                ConnectionString = DatabaseUtils.GetConnectionString(),
                                Schema = "main",
                                Table = "EmptyTable",
                                IdentifierColumn = "Id",
                                GeometryColumn = "Geom",
                                GeometryDataType = "geometry",
                                GeometrySrid = 3857,
                                Properties = ["Name"]
                            }
                        }
                    },

                    new CollectionOptions
                    {
                        Id = "PolygonsForInsert",
                        Features = new CollectionFeaturesOptions
                        {
                            Crs =
                            [
                                new Uri("http://www.opengis.net/def/crs/OGC/1.3/CRS84"),
                                new Uri("http://www.opengis.net/def/crs/EPSG/0/3857")
                            ],
                            StorageCrs = new Uri("http://www.opengis.net/def/crs/EPSG/0/3857"),
                            Storage = new SqlFeaturesSourceOptions
                            {
                                Type = "SQLite",
                                ConnectionString = DatabaseUtils.GetConnectionString(),
                                Schema = "main",
                                Table = "PolygonsForInsert",
                                IdentifierColumn = "Id",
                                GeometryColumn = "Geom",
                                GeometryDataType = "geometry",
                                GeometrySrid = 3857,
                                DateTimeColumn = "Date",
                                Properties =
                                [
                                    "Name",
                                    "Number",
                                    "S",
                                    "Date"
                                ]
                            }
                        }
                    }
                ]
            }
        };
    }

    private static OgcApiOptions GetOptionsWithUnknownTable()
    {
        return new OgcApiOptions
        {
            Collections = new CollectionsOptions
            {
                Items =
                [
                    new CollectionOptions
                    {
                        Id = "Test",
                        Features = new CollectionFeaturesOptions
                        {
                            Crs =
                            [
                                new Uri("http://www.opengis.net/def/crs/OGC/1.3/CRS84"),
                                new Uri("http://www.opengis.net/def/crs/EPSG/0/3857")
                            ],
                            StorageCrs = new Uri("http://www.opengis.net/def/crs/EPSG/0/3857"),
                            Storage = new SqlFeaturesSourceOptions
                            {
                                Type = "SQLite",
                                ConnectionString = DatabaseUtils.GetConnectionString(),
                                Schema = "main",
                                Table = "Test",
                                IdentifierColumn = "Id",
                                GeometryColumn = "Geom",
                                GeometryDataType = "geometry",
                                GeometrySrid = 3857
                            }

                        }

                    }
                ]
            }
        };
    }

    private static OgcApiOptions GetOptionsWithApiKey()
    {
        return new OgcApiOptions
        {
            Collections = new CollectionsOptions
            {
                Items =
                [
                    new CollectionOptions
                    {
                        Id = "PointsWithApiKey",
                        Features = new CollectionFeaturesOptions
                        {
                            Crs =
                            [
                                new Uri("http://www.opengis.net/def/crs/OGC/1.3/CRS84"),
                                new Uri("http://www.opengis.net/def/crs/EPSG/0/3857")
                            ],
                            StorageCrs = new Uri("http://www.opengis.net/def/crs/EPSG/0/3857"),
                            Storage = new SqlFeaturesSourceOptions
                            {
                                Type = "SQLite",
                                ConnectionString = DatabaseUtils.GetConnectionString(),
                                Schema = "main",
                                Table = "PointsWithApiKey",
                                IdentifierColumn = "Id",
                                GeometryColumn = "Geom",
                                GeometryDataType = "geometry",
                                GeometrySrid = 3857,
                                Properties = ["Name"],
                                ApiKeyPredicateForGet = "[Key] = @ApiKey"
                            }
                        }
                    }
                ]
            }
        };
    }


    public static SQLiteProvider GetDefaultProvider()
    {
        return new SQLiteProvider(new NullLogger<SQLiteProvider>(),
            Mock.Of<IOptionsMonitor<OgcApiOptions>>(monitor => monitor.CurrentValue == GetDefaultOptions()));
    }
    public static SQLiteProvider GetProviderWithErrors()
    {
        return new SQLiteProvider(new NullLogger<SQLiteProvider>(),
            Mock.Of<IOptionsMonitor<OgcApiOptions>>(monitor => monitor.CurrentValue == GetOptionsWithUnknownTable()));
    }
    public static SQLiteProvider GetProviderWithApiKey()
    {
        return new SQLiteProvider(new NullLogger<SQLiteProvider>(),
            Mock.Of<IOptionsMonitor<OgcApiOptions>>(monitor => monitor.CurrentValue == GetOptionsWithApiKey()));
    }
}
