using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Comic : Entity
{
    public virtual string Title { get; set; } = null!;
    public virtual string Series { get; set; } = null!;
    public virtual string Pages { get; set; } = null!;
    public virtual string ScaledPages { get; set; } = null!;
    public virtual string Credits { get; set; } = null!;
    public virtual string Description { get; set; } = null!;
    public virtual ISet<Champion> FeaturedChampions { get; set; } = new HashSet<Champion>();
}