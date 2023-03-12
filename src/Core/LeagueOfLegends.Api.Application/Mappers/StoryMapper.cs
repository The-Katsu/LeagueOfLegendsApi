using LeagueOfLegends.Api.Domain.Contracts.Responses.Story;
using LeagueOfLegends.Api.Domain.Entities;

namespace LeagueOfLegends.Api.Application.Mappers;

public static class StoryMapper
{
    private static StoryChampionResponse ToStoryChampionResponse(this Champion champion) =>
        new()
        {
            Id = champion.Id,
            Name = champion.Name
        };

    public static StoryResponseWithDetails ToStoryResponseWithDetails(this Story story) =>
        new()
        {
            Id = story.Id,
            Title = story.Title,
            Subtitle = story.Subtitle,
            ReleaseDate = DateOnly.FromDateTime(story.ReleaseDate).ToShortDateString(),
            ImageUrl = story.ImageUrl,
            Url = story.Url,
            Content = story.Content,
            FeaturedChampions = story.FeaturedChampions.Select(ToStoryChampionResponse).ToList()
        };

    public static StoryResponse ToStoryResponse(this Story story) =>
        new()
        {
            Id = story.Id,
            Title = story.Title,
            Url = story.Url
        };
    
}