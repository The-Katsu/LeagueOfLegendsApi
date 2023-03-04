using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Race : Entity
{
    public virtual string Name { get; set; } = string.Empty;

    public virtual ISet<Champion> Champions { get; set; } = new HashSet<Champion>();
}