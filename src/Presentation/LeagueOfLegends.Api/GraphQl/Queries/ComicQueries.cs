using LeagueOfLegends.Api.Domain.Entities;
using IQueryProvider = LeagueOfLegends.Api.Application.GraphQl.IQueryProvider;

namespace LeagueOfLegends.Api.GraphQl.Queries;

[ExtendObjectType("Query")]
public class ComicQueries
{
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    public IQueryable<Comic> Comic([Service] IQueryProvider queryProvider) =>
        queryProvider.Comics;
    
    [UseOffsetPaging(DefaultPageSize = 20, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Comic> Comics([Service] IQueryProvider queryProvider) =>
        queryProvider.Comics;
}