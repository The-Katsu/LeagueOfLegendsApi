using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class RelatedStory : Entity
{
    public virtual Story Story { get; set; } = null!;
}