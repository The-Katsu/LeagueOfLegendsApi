namespace LeagueOfLegends.Api.Application.Contracts.Responses.Champion;

public class ChampionSkinResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string ImageUrl { get; init; } = null!;
}