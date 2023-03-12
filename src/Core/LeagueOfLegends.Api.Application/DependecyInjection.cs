using LeagueOfLegends.Api.Application.GraphQl;
using LeagueOfLegends.Api.Application.Jobs.Crawler.Parser;
using LeagueOfLegends.Api.Application.Services;
using LeagueOfLegends.Api.Application.Services.Implementations;
using LeagueOfLegends.Api.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IQueryProvider = LeagueOfLegends.Api.Application.GraphQl.IQueryProvider;

namespace LeagueOfLegends.Api.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ILolParser, LolParser>();
        services.AddHttpClient<ILolParser, LolParser>(client => 
            client.DefaultRequestHeaders.Add("User-Agent", "My-Agent"));

        services.AddScoped<IChampionService, ChampionService>();
        services.AddScoped<IVideoService, VideoService>();
        services.AddScoped<IStoryService, StoryService>();
        services.AddScoped<IComicService, ComicService>();
        services.AddScoped<IRegionService, RegionService>();
        services.AddScoped<IQueryProvider, QueryProvider>();
        
        return services;
    }
}