using LeagueOfLegends.Api.Application.Parser;
using LeagueOfLegends.Api.Application.Services.Implementations;
using LeagueOfLegends.Api.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeagueOfLegends.Api.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILolParser, LolParser>();
        services.AddHttpClient<ILolParser, LolParser>(client =>
        {
            client.DefaultRequestHeaders.Add("User-Agent", "My-Agent");
        });

        services.AddScoped<IChampionService, ChampionService>();
        
        return services;
    }
}