using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Story : Entity
{
    public virtual DateTime ReleaseDate { get; set; } 
    public virtual string Title { get; set; } = null!;
    public virtual int WordCount { get; set; }
    public virtual int MinutesToRead { get; set; }
    public virtual string Subtitle { get; set; } = null!;
    public virtual string ImageUrl { get; set; } = null!;
    public virtual string Content { get; set; } = null!;

    public virtual Region Region { get; set; } = null!;
    public virtual ISet<Champion> FeaturedChampions { get; set; } = new HashSet<Champion>();
}