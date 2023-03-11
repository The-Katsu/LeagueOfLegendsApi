namespace LeagueOfLegends.Api.Domain.Contracts.Responses.Comic;

public class ComicResponse
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;
    public string Url { get; init; } = null!;
}