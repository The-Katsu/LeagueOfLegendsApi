using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Region : Entity
{
    public virtual string Name { get; set; } = string.Empty;
    public virtual string ImageUrl { get; set; } = string.Empty;
    public virtual string AnimatedImageUrl { get; set; } = string.Empty;
    public virtual string Overview { get; set; } = string.Empty;
    
    public virtual ISet<RelatedStory> RelatedStories { get; set; } = new HashSet<RelatedStory>();
    public virtual ISet<Champion> AssociatedChampions { get; set; } = new HashSet<Champion>();
}