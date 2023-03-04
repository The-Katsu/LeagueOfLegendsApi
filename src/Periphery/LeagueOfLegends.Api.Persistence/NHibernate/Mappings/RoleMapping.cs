using LeagueOfLegends.Api.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Mappings;

public class RoleMapping : ClassMapping<Role>
{
    public RoleMapping()
    {
        Table("role");
        
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
                m.Table("champion_roles");
                m.Cascade(Cascade.None);
                m.Key(k => k.Column("role_id"));
            }, 
            r => r.ManyToMany(m => m.Column("champion_id"))
        );
    }
}