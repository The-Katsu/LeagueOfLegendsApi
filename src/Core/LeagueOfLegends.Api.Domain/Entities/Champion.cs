using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Champion : Entity
{
    public virtual string ImageUrl { get; set; } = null!;
    public virtual string AnimatedImageUrl { get; set; } = null!;
    public virtual string Name { get; set; } = null!;
    public virtual string Nickname { get; set; } = null!;
    public virtual string Biography { get; set; } = null!;
    public virtual Region Region { get; set; } = null!;
    public virtual ISet<Comic> FeaturedComic { get; set; } = new HashSet<Comic>();
    public virtual ISet<Story> FeaturedStories { get; set; } = new HashSet<Story>();
    public virtual ISet<Video> FeaturedVideos { get; set; } = new HashSet<Video>();
    public virtual ISet<Race> Races { get; set; } = new HashSet<Race>();
    public virtual ISet<Role> Roles { get; set; } = new HashSet<Role>();
    public virtual ISet<Skin> Skins { get; set; } = new HashSet<Skin>();
    public virtual ISet<RelatedChampion> RelatedChampions { get; set; } = new HashSet<RelatedChampion>();
    public virtual ISet<Ability> Abilities { get; set; } = new HashSet<Ability>();
}