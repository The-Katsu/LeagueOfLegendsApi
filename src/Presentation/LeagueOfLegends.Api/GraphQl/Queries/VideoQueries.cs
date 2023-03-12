using LeagueOfLegends.Api.Domain.Entities;
using IQueryProvider = LeagueOfLegends.Api.Application.GraphQl.IQueryProvider;

namespace LeagueOfLegends.Api.GraphQl.Queries;

[ExtendObjectType("Query")]
public class VideoQueries
{
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    public IQueryable<Video> Video([Service] IQueryProvider queryProvider) =>
        queryProvider.Videos;
    
    [UseOffsetPaging(DefaultPageSize = 20, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Video> Videos([Service] IQueryProvider queryProvider) =>
        queryProvider.Videos;
}