namespace LeagueOfLegends.Api.Domain.Contracts.Responses.Story;

public class StoryResponse
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;
    public string Url { get; init; } = null!;
}