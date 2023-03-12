namespace LeagueOfLegends.Api.Domain.Contracts.Responses.Story;

public class StoryResponseWithDetails
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;
    public string Subtitle { get; init; } = null!;
    public string ReleaseDate { get; init; }
    public string ImageUrl { get; init; } = null!;
    public string Url { get; init; } = null!;
    public string Content { get; init; } = null!;
    public IList<StoryChampionResponse> FeaturedChampions { get; init; } = new List<StoryChampionResponse>();
}