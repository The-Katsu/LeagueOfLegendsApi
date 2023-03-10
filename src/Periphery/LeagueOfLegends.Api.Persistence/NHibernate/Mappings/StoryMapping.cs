using LeagueOfLegends.Api.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Mappings;

public class StoryMapping : ClassMapping<Story>
{
    public StoryMapping()
    {
        Table("story");
        
        Id(x => x.Id, m => m.Generator(Generators.Native));
        
        Property(x => x.ReleaseDate, m => m.Column("release_date"));
        Property(x => x.Title, m =>
        {
            m.Column("title");
            m.Unique(true);
        });
        Property(x => x.WordCount, m => m.Column("word_count"));
        Property(x => x.MinutesToRead, m => m.Column("minutes_to_read"));
        Property(x => x.Subtitle, m => m.Column("subtitle"));
        Property(x => x.ImageUrl, m => m.Column("image_url"));
        Property(x => x.Content, m =>
        {
            m.Column("content");
            m.Type(NHibernateUtil.StringClob);
        });
        
        Set(x => x.FeaturedChampions,
            m =>
            {
                m.Table("champion_story");
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("champion_id"));
                m.Lazy(CollectionLazy.Lazy);
            },
            r => 
                r.ManyToMany(m => m.Column("story_id")));
        
        ManyToOne(x => x.Region, 
            m =>
            {
                m.Cascade(Cascade.All);
                m.Column("region_id");
            });
    }
}