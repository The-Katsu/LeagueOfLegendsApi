using LeagueOfLegends.Api.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Mappings;

public class VideoMapping : ClassMapping<Video>
{
    public VideoMapping()
    {
        Table("cinematic");
        
        Id(x => x.Id, m => m.Generator(Generators.Native));

        Property(x => x.Title, m =>
        {
            m.Column("title");
            m.Unique(true);
        });
        Property(x => x.Description, m => m.Column("description"));
        Property(x => x.ImageUrl, m => m.Column("image_url"));
        Property(x => x.Url, m => m.Column("url"));
        Property(x => x.Type, m => m.Column("type"));
        
        Set(x => x.FeaturedChampions,
            m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("video_id"));
                m.Table("champion_video");
                m.Lazy(CollectionLazy.Lazy);
            },
            r => 
                r.ManyToMany(m => m.Column("champion_id")));
    }
}