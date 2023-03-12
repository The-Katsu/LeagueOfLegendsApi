using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Ability : Entity
{
    public virtual string Description { get; set; } = null!;
    public virtual string IconUrl { get; set; } = null!;
    public virtual string Name { get; set; } = null!;
    public virtual string Mp4Url { get; set; } = null!;
    public virtual string WebmUrl { get; set; } = null!;
    public virtual Champion? Champion { get; set; } = null!;
}