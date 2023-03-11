namespace LeagueOfLegends.Api.Domain.Contracts.Responses.Champion;

public class ChampionResponseWithDetails
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Nickname { get; init; } = null!;
    public string ImageUrl { get; init; } = null!;
    public string AnimatedImageUrl { get; init; } = null!;
    public DateTime ReleaseDate { get; init; }
    public string Biography { get; init; } = null!;
    public ChampionRegionResponse Region { get; set; } = null!;
    public IList<ChampionAbilityResponse> Abilities { get; init; } = new List<ChampionAbilityResponse>();
    public IList<ChampionRoleResponse> Roles { get; init; } = new List<ChampionRoleResponse>();
    public IList<ChampionRaceResponse> Races { get; init; } = new List<ChampionRaceResponse>();
    public IList<ChampionSkinResponse> Skins { get; init; } = new List<ChampionSkinResponse>();
    public IList<ChampionRelatedChampionResponse> RelatedChampions { get; init; } = new List<ChampionRelatedChampionResponse>();
    public IList<ChampionComicResponse> FeaturedComics { get; init; } = new List<ChampionComicResponse>();
    public IList<ChampionStoryResponse> FeaturedStories { get; init; } = new List<ChampionStoryResponse>();
    public IList<ChampionVideoResponse> FeaturedVideos { get; init; } = new List<ChampionVideoResponse>();
}