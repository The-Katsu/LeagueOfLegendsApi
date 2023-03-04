using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Story : Entity
{
    public virtual DateTime ReleaseDate { get; set; }
    public virtual string Title { get; set; } = string.Empty;
    public virtual int WordCount { get; set; }
    public virtual int MinutesToRead { get; set; }
    public virtual string Subtitle { get; set; } = string.Empty;
    public virtual string ImageUrl { get; set; } = string.Empty;
    public virtual string Content { get; set; } = string.Empty;

    public virtual Region Region { get; set; } = null!;
    public virtual ISet<Champion> FeaturedChampions { get; set; } = new HashSet<Champion>();
}