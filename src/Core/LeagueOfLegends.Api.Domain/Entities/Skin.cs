using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Skin : Entity
{
    public virtual string Name { get; set; } = null!;
    public virtual string ImageUrl { get; set; } = null!;
    public virtual Champion? Champion { get; set; } = null!;
}