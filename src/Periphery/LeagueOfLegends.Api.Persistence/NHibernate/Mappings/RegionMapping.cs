using LeagueOfLegends.Api.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Mappings;

public class RegionMapping : ClassMapping<Region>
{
    public RegionMapping()
    {
        Table("region");
        
        Id(x => x.Id, m =>
        {
            m.Generator(Generators.Guid);
            m.Type(NHibernateUtil.Guid);
            m.Column("id");
            m.UnsavedValue(Guid.Empty);
        });
        
        Property(x => x.Name, m => m.Column("name"));
        Property(x => x.ImageUrl, m => m.Column("image_url"));
        Property(x => x.Overview, m => m.Column("overview"));
        Property(x => x.AnimatedImageUrl, m => m.Column("animated_image_url"));
        
        Set(x => x.AssociatedChampions, 
            m => m.Key(k => k.Column("region_id")),
            r => r.OneToMany());
        Set(x => x.RelatedStories,
            m => m.Key(k => k.Column("story_id")),
            r => r.OneToMany());
    }
}