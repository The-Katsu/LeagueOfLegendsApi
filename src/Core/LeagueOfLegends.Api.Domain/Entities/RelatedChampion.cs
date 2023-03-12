using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class RelatedChampion : Entity
{
    public virtual Champion? Champion { get; set; } = null!;
}