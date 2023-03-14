using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.GraphQl.Queries;

[ExtendObjectType("Query")]
public class RegionQueries
{
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    public IQueryable<Region> Region([Service] IRegionService regionService) =>
        regionService.GetQuery();
    
    [UseOffsetPaging(DefaultPageSize = 20, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Region> Regions([Service] IRegionService regionService) =>
        regionService.GetQuery();
}