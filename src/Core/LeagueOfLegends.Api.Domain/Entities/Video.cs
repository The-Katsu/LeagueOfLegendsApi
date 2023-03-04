using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Video : Entity
{
    public virtual string Title { get; set; } = string.Empty;
    public virtual string Subtitle { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;
    public virtual string ImageUrl { get; set; } = string.Empty;
    public virtual string Url { get; set; } = string.Empty;
    public virtual string Type { get; set; } = string.Empty;

    public virtual ISet<Champion> FeaturedChampions { get; set; } = new HashSet<Champion>();
}