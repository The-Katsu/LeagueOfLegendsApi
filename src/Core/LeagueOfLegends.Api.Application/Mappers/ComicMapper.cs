using LeagueOfLegends.Api.Domain.Contracts.Responses.Comic;
using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Application.Mappers;

public static class ComicMapper
{
    private static ComicChampionResponse ToComicChampionResponse(this Champion champion) =>
        new()
        {
            Id = champion.Id,
            Name = champion.Name
        };

    public static ComicResponseWithDetails ToComicResponseWithDetails(this Comic comic) =>
        new()
        {
            Id = comic.Id,
            Title = comic.Title,
            Description = comic.Description,
            Url = comic.Url,
            Credits = comic.Credits.Split('\n').ToList(),
            Content = comic.Content.Split('\n').ToList(),
            FeaturedChampions = comic.FeaturedChampions.Select(ToComicChampionResponse)
                .ToList()
        };

    public static ComicResponse ToComicResponse(this Comic comic) =>
        new()
        {
            Id = comic.Id,
            Title = comic.Title,
            Url = comic.Url
        };
}