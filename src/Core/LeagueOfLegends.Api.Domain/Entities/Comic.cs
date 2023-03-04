using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Comic : Entity
{
    public virtual string Title { get; set; } = string.Empty;
    public virtual string Series { get; set; } = string.Empty;
    public virtual string Pages { get; set; } = string.Empty;
    public virtual string ScaledPages { get; set; } = string.Empty;
    public virtual string Credits { get; set; } = string.Empty;
    public virtual string Description { get; set; } = string.Empty;
    public virtual ISet<Champion> FeaturedChampions { get; set; } = new HashSet<Champion>();
}