using Microsoft.Extensions.DependencyInjection;
using OgcApi.Net.DataProviders;

namespace OgcApi.Net.SQLite;

public static class OgcApiServiceCollectionExtensions
{
    public static IServiceCollection AddOgcApiSQLiteProvider(this IServiceCollection services)
    {
        services.AddSingleton<IFeaturesProvider, SQLiteProvider>();
        services.AddSingleton<ITilesProvider, SQLiteProvider>();

        return services;
    }
}