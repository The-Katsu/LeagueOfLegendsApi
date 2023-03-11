namespace LeagueOfLegends.Api.Domain.Contracts.Responses.Champion;

public class ChampionStoryResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Url { get; set; } = null!;
}