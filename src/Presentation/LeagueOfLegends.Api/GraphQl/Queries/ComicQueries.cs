using LeagueOfLegends.Api.Application.Services.Interfaces;
using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.GraphQl.Queries;

[ExtendObjectType("Query")]
public class ComicQueries
{
    [UseFirstOrDefault]
    [UseProjection]
    [UseFiltering]
    public IQueryable<Comic> Comic([Service] IComicService comicService) =>
        comicService.GetQuery();
    
    [UseOffsetPaging(DefaultPageSize = 20, IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Comic> Comics([Service] IComicService comicService) =>
        comicService.GetQuery();
}