using OgcApi.SQLite.Tests.Utils;

namespace OgcApi.SQLite.Tests;

public class DatabaseFixture
{
    public DatabaseFixture()
    {
        DatabaseUtils.RecreateDatabase();
    }
}