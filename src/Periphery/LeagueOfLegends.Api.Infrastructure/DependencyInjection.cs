using LeagueOfLegends.Api.Infrastructure.Extensions;
using LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeagueOfLegends.Api.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IChampionRepository, ChampionRepository>().AllowLazy<IChampionRepository>();
        services.AddScoped<IAbilityRepository, AbilityRepository>().AllowLazy<IAbilityRepository>();
        services.AddScoped<IComicRepository, ComicRepository>().AllowLazy<IComicRepository>();
        services.AddScoped<IRaceRepository, RaceRepository>().AllowLazy<IRaceRepository>();
        services.AddScoped<IRegionRepository, RegionRepository>().AllowLazy<IRegionRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>().AllowLazy<IRoleRepository>();
        services.AddScoped<ISkinRepository, SkinRepository>().AllowLazy<ISkinRepository>();
        services.AddScoped<IStoryRepository, StoryRepository>().AllowLazy<IStoryRepository>();
        services.AddScoped<IVideoRepository, VideoRepository>().AllowLazy<IVideoRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}