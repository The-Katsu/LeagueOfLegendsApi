using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Region : Entity
{
    public virtual string Name { get; set; } = null!;
    public virtual string ImageUrl { get; set; } = null!;
    public virtual string AnimatedImageUrl { get; set; } = null!;
    public virtual string Overview { get; set; } = null!;
    
    public virtual ISet<RelatedStory> RelatedStories { get; set; } = new HashSet<RelatedStory>();
    public virtual ISet<Champion> AssociatedChampions { get; set; } = new HashSet<Champion>();
}