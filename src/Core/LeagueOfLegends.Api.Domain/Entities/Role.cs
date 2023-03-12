﻿using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Domain.Entities;

public class Role : Entity
{
    public virtual string Name { get; set; } = null!;

    public virtual ISet<Champion> Champions { get; set; } = new HashSet<Champion>();
}