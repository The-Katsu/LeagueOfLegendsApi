using LeagueOfLegends.Api.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Mappings;

public class RaceMapping : ClassMapping<Race>
{
    public RaceMapping()
    {
        Table("race");
        
        Id(x => x.Id, m =>
        {
            m.Generator(Generators.Guid);
            m.Type(NHibernateUtil.Guid);
            m.Column("id");
            m.UnsavedValue(Guid.Empty);
        });
        
        Property(x => x.Name, m => m.Column("name"));
        
        Set(x => x.Champions, m =>
            {
                m.Table("champion_races");
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("race_id"));
                m.Lazy(CollectionLazy.Lazy);
            }, 
            r => r.ManyToMany(m => m.Column("champion_id"))
        );
    }
}