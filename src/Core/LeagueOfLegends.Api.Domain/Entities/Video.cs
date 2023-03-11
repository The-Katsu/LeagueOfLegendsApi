using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Video : Entity
{
    public virtual string Title { get; set; } = null!;
    public virtual string Description { get; set; } = null!;
    public virtual string ImageUrl { get; set; } = null!;
    public virtual string Url { get; set; } = null!;
    public virtual string Type { get; set; } = null!;

    public virtual ISet<Champion> FeaturedChampions { get; set; } = new HashSet<Champion>();
}