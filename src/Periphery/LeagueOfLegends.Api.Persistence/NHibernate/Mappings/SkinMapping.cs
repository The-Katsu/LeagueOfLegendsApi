using LeagueOfLegends.Api.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Mappings;

public class SkinMapping : ClassMapping<Skin>
{
    public SkinMapping()
    {
        Table("skin");
        
        Id(x => x.Id, m =>
        {
            m.Generator(Generators.Guid);
            m.Type(NHibernateUtil.Guid);
            m.Column("id");
            m.UnsavedValue(Guid.Empty);
        });
        
        Property(x => x.Name, m => m.Column("name"));
        Property(x => x.ImageUrl, m => m.Column("image_url"));
        
        ManyToOne(x => x.Champion, m =>
        {
            m.Cascade(Cascade.All);
            m.Column("champion_id");
        });
    }
}