using LeagueOfLegends.Api.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Mappings;

public class ChampionMapping : ClassMapping<Champion>
{
    public ChampionMapping()
    {
        Table("champion");
        
        Id(x => x.Id, m => m.Generator(Generators.Identity));
        
        Property(x => x.AnimatedImageUrl, m => m.Column("animated_image_url"));
        Property(x => x.ImageUrl, m => m.Column("image_url"));
        Property(x => x.Name, m =>
        {
            m.Column("name");
            m.Unique(true);
        });
        Property(x => x.ReleaseDate, m => m.Column("release_date"));
        Property(x => x.Nickname, m => m.Column("nickname"));
        Property(x => x.Biography, m =>
        {
            m.Column("biography");
            m.Type(NHibernateUtil.StringClob);
        });
        
        ManyToOne(x => x.Region, m =>
        {
            m.Cascade(Cascade.All);
            m.Column("region_id");
        });
        
        Set(x => x.Races, m =>
            {
                m.Table("champion_races");
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("champion_id"));
                m.Lazy(CollectionLazy.Lazy);
            }, 
            r => r.ManyToMany(m => m.Column("race_id"))
            );
        Set(x => x.Roles, m =>
            {
                m.Table("champion_roles");
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("champion_id"));
                m.Lazy(CollectionLazy.Lazy);
            }, 
            r => r.ManyToMany(m => m.Column("role_id"))
        );
        Set(x => x.FeaturedVideos,
            m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("video_id"));
                m.Table("champion_video");
                m.Lazy(CollectionLazy.Lazy);
            },
            r => 
                r.ManyToMany(m => m.Column("champion_id")));
        Set(x => x.FeaturedStories,
            m =>
            {
                m.Table("champion_story");
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("story_id"));
                m.Lazy(CollectionLazy.Lazy);
            },
            r => 
                r.ManyToMany(m => m.Column("champion_id")));
        Set(x => x.FeaturedComic,
            m =>
            {
                m.Table("champion_comic");
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("comic_id"));
                m.Lazy(CollectionLazy.Lazy);
            },
            r => 
                r.ManyToMany(m => m.Column("champion_id")));

        
        
        Set(x => x.Skins,
            m =>
            {
                m.Key(k => k.Column("champion_id"));
                m.Lazy(CollectionLazy.Lazy);
                m.Cascade(Cascade.All);
            },
            r => r.OneToMany());
        Set(x => x.RelatedChampions,
            m =>
            {
                m.Key(k => k.Column("related_champion_id"));
                m.Lazy(CollectionLazy.Lazy);
                m.Cascade(Cascade.All);
            },
            r => r.OneToMany());
        Set(x => x.Abilities,
            m =>
            {
                m.Key(k => k.Column("champion_id"));
                m.Lazy(CollectionLazy.Lazy);
                m.Cascade(Cascade.All);
            },
            r => r.OneToMany());
    }
}