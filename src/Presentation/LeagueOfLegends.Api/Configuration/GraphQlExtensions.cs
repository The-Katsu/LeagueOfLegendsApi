using LeagueOfLegends.Api.GraphQl.Queries;

namespace LeagueOfLegends.Api.Configuration;

public static class GraphQlExtensions
{
    public static IServiceCollection AddGraphQl(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType(q => q.Name("Query"))
            .AddType<ChampionQueries>()
            .AddType<RegionQueries>()
            .AddType<ComicQueries>()
            .AddType<StoryQueries>()
            .AddType<VideoQueries>()
            .AddSorting()
            .AddFiltering()
            .AddProjections();

        return services;
    }
}