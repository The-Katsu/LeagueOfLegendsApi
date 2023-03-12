namespace LeagueOfLegends.Api.Domain.Contracts.Responses.Comic;

public class ComicResponseWithDetails
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string Url { get; init; } = null!;
    public List<string> Credits { get; init; } = null!;
    public List<string> Content { get; init; } = null!;
    public IList<ComicChampionResponse> FeaturedChampions { get; init; } = new List<ComicChampionResponse>();
}