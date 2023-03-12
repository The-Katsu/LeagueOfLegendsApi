using LeagueOfLegends.Api.Domain.Entities;
using IQueryProvider = LeagueOfLegends.Api.Application.GraphQl.IQueryProvider;

namespace LeagueOfLegends.Api.GraphQl.Queries;

[ExtendObjectType("Query")]
public class ChampionQueries
{
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    public IQueryable<Champion> Champion([Service] IQueryProvider queryProvider) =>
        queryProvider.Champions;
    
    [UseOffsetPaging(DefaultPageSize = 20, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Champion> Champions([Service] IQueryProvider queryProvider) =>
        queryProvider.Champions;
}