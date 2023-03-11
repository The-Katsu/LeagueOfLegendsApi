namespace LeagueOfLegends.Api.Domain.Contracts.Responses.Video;

public class VideoResponseWithDetails
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string ImageUrl { get; init; } = null!;
    public string Url { get; init; } = null!;
    public IList<VideoChampionResponse> FeaturedChampions { get; init; } = new List<VideoChampionResponse>();
}