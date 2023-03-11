using LeagueOfLegends.Api.Domain.Contracts.Responses.Video;
using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Application.Mappers;

public static class VideoMapper
{
    private static VideoChampionResponse ToVideoChampionResponse(this Champion champion) =>
        new()
        {
            Id = champion.Id,
            Name = champion.Name
        };

    public static VideoResponseWithDetails ToVideoResponseWithDetails(this Video video) =>
        new()
        {
            Id = video.Id,
            Title = video.Title,
            Description = video.Description,
            ImageUrl = video.ImageUrl,
            Url = video.Url,
            FeaturedChampions = video.FeaturedChampions.Select(ToVideoChampionResponse).ToList()
        };

    public static VideoResponse ToVideoResponse(this Video video) =>
        new()
        {
            Id = video.Id,
            Title = video.Title,
            Url = video.Url
        };
}