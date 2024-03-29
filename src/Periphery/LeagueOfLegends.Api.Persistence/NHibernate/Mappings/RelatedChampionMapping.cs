﻿using LeagueOfLegends.Api.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Mappings;

public class RelatedChampionMapping : ClassMapping<RelatedChampion>
{
    public RelatedChampionMapping()
    {
        Table("related_champion");

        Id(x => x.Id, m => m.Generator(Generators.Native));
        
        ManyToOne(x => x.Champion,
            m =>
            {
                m.Column("champion_id");
                m.Cascade(Cascade.All);
            });
    }
}