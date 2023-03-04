using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Ability : Entity
{
    public virtual string Description { get; set; } = string.Empty;
    public virtual string IconUrl { get; set; } = string.Empty;
    public virtual string Name { get; set; } = string.Empty;
    public virtual string Mp4Url { get; set; } = string.Empty;
    public virtual string WebmUrl { get; set; } = string.Empty;
    public virtual Champion Champion { get; set; } = null!;
}