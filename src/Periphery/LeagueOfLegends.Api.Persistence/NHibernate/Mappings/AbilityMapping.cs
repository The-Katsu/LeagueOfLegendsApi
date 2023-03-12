using LeagueOfLegends.Api.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Mappings;

public class AbilityMapping : ClassMapping<Ability>
{
    public AbilityMapping()
    {
        Table("ability");
        
        Id(x => x.Id, m => m.Generator(Generators.Native));
        
        Property(x => x.Description, m =>
        {
            m.Column("description");
            m.Type(NHibernateUtil.StringClob);
        });
        Property(x => x.IconUrl, m => m.Column("icon_url"));
        Property(x => x.Name, m =>
        {
            m.Column("name");
            m.Unique(true);
        });
        Property(x => x.Mp4Url, m => m.Column("mp4_url"));
        Property(x => x.WebmUrl, m => m.Column("webm_url"));
        
        ManyToOne(x => x.Champion,
            m =>
            {
                m.Cascade(Cascade.All);
                m.Column("champion_id");
            });
    }
}