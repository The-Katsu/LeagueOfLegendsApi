using LeagueOfLegends.Api.Application.Parser;
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
        return services;
    }
}