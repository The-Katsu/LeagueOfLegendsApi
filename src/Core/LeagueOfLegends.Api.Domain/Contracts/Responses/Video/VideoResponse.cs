namespace LeagueOfLegends.Api.Domain.Contracts.Responses.Video;

public class VideoResponse
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;
    public string Url { get; init; } = null!;
}