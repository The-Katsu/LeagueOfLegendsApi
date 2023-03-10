namespace LeagueOfLegends.Api.Application.Contracts.Responses.Champion;

public class ChampionResponseWithDetails
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Nickname { get; init; } = null!;
    public string ImageUrl { get; init; } = null!;
    public string AnimatedImageUrl { get; init; } = null!;
    public DateTime ReleaseDate { get; init; }
    public string Biography { get; init; } = null!;
    public IList<ChampionRoleResponse> Roles { get; init; } = new List<ChampionRoleResponse>();
    public IList<ChampionAbilityResponse> Abilities { get; init; } = new List<ChampionAbilityResponse>();
    public IList<ChampionRaceResponse> Races { get; init; } = new List<ChampionRaceResponse>();
    public IList<ChampionSkinResponse> Skins { get; init; } = new List<ChampionSkinResponse>();
    public IList<ChampionRelatedChampionResponse> RelatedChampions { get; init; } = new List<ChampionRelatedChampionResponse>();
}