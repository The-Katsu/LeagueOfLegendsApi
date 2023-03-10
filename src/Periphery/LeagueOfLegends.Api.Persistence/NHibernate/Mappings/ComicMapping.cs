using LeagueOfLegends.Api.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Mappings;

public class ComicMapping : ClassMapping<Comic>
{
    public ComicMapping()
    {
        Table("comic");
        
        Id(x => x.Id, m => m.Generator(Generators.Native));

        Property(x => x.Title, m =>
        {
            m.Column("title");
            m.Unique(true);
        });
        Property(x => x.Series, m => m.Column("series"));
        Property(x => x.Pages, m => m.Column("pages"));
        Property(x => x.ScaledPages, m => m.Column("scaled_pages"));
        Property(x => x.Credits, m => m.Column("credits"));
        Property(x => x.Description, m =>
        {
            m.Column("description");
            m.Type(NHibernateUtil.StringClob);
        });
        
        Set(x => x.FeaturedChampions,
            m =>
            {
                m.Table("champion_comic");
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("champion_id"));
                m.Lazy(CollectionLazy.Lazy);
            },
            r => 
                r.ManyToMany(m => m.Column("comic_id")));
    }
}