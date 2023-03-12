using LeagueOfLegends.Api.Domain.Entities;
using IQueryProvider = LeagueOfLegends.Api.Application.GraphQl.IQueryProvider;

namespace LeagueOfLegends.Api.GraphQl.Queries;

[ExtendObjectType("Query")]
public class RegionQueries
{
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    public IQueryable<Region> Region([Service] IQueryProvider queryProvider) =>
        queryProvider.Regions;
    
    [UseOffsetPaging(DefaultPageSize = 20, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Region> Regions([Service] IQueryProvider queryProvider) =>
        queryProvider.Regions;
}