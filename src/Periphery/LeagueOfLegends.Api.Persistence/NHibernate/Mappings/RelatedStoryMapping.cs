using LeagueOfLegends.Api.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Mappings;

public class RelatedStoryMapping : ClassMapping<RelatedStory>
{
    public RelatedStoryMapping()
    {
        Table("related_story");

        Id(x => x.Id, m =>
        {
            m.Generator(Generators.Guid);
            m.Type(NHibernateUtil.Guid);
            m.Column("id");
            m.UnsavedValue(Guid.Empty);
        });
        
        ManyToOne(x => x.Story,
            m =>
            {
                m.Column("champion_id");
                m.Cascade(Cascade.None);
            });
    }
}