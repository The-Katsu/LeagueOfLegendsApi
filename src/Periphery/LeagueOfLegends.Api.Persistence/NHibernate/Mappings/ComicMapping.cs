using LeagueOfLegends.Api.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Tuple;

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
        Property(x => x.Url, m => m.Column("url"));
        Property(x => x.Content, m =>
        {
            m.Column("content");
            m.Type(NHibernateUtil.StringClob);
        });
        Property(x => x.Credits, m =>
        {
            m.Column("credits");
            m.Type(NHibernateUtil.StringClob);
        });
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
                m.Key(k => k.Column("comic_id"));
                m.Lazy(CollectionLazy.Lazy);
            },
            r => 
                r.ManyToMany(m => m.Column("champion_id")));
    }
}