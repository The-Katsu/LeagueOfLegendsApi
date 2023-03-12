using LeagueOfLegends.Api.Domain.Entities;
using IQueryProvider = LeagueOfLegends.Api.Application.GraphQl.IQueryProvider;

namespace LeagueOfLegends.Api.GraphQl.Queries;

[ExtendObjectType("Query")]
public class StoryQueries
{
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    public IQueryable<Story> Story([Service] IQueryProvider queryProvider) =>
        queryProvider.Stories;
    
    [UseOffsetPaging(DefaultPageSize = 20, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Story> Stories([Service] IQueryProvider queryProvider) =>
        queryProvider.Stories;
}