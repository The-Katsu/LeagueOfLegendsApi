using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Comic : Entity
{
    public virtual string Title { get; set; } = null!;
    public virtual string Url { get; set; } = null!;
    public virtual string Content { get; set; } = null!;
    public virtual string Credits { get; set; } = null!;
    public virtual string Description { get; set; } = null!;
    public virtual ISet<Champion> FeaturedChampions { get; set; } = new HashSet<Champion>();
}