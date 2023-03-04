using LeagueOfLegends.Api.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Mappings;

public class ChampionMapping : ClassMapping<Champion>
{
    public ChampionMapping()
    {
        Table("champion");
        
        Id(x => x.Id, m =>
        {
            m.Generator(Generators.Guid);
            m.Type(NHibernateUtil.Guid);
            m.Column("id");
            m.UnsavedValue(Guid.Empty);
        });
        
        Property(x => x.AnimatedImageUrl, m => m.Column("animated_image_url"));
        Property(x => x.ImageUrl, m => m.Column("image_url"));
        Property(x => x.Name, m => m.Column("name"));
        Property(x => x.Nickname, m => m.Column("nickname"));
        Property(x => x.Biography, m => m.Column("biography"));
        
        ManyToOne(x => x.Region, m =>
        {
            m.Cascade(Cascade.None);
            m.Column("region_id");            
        });
        
        Set(x => x.Races, m =>
            {
                m.Table("champion_races");
                m.Cascade(Cascade.None);
                m.Key(k => k.Column("champion_id"));
            }, 
            r => r.ManyToMany(m => m.Column("race_id"))
            );
        Set(x => x.Roles, m =>
            {
                m.Table("champion_roles");
                m.Cascade(Cascade.None);
                m.Key(k => k.Column("champion_id"));
            }, 
            r => r.ManyToMany(m => m.Column("role_id"))
        );
        Set(x => x.FeaturedVideos,
            m =>
            {
                m.Cascade(Cascade.None);
                m.Key(k => k.Column("video_id"));
                m.Table("champion_video");
            },
            r => 
                r.ManyToMany(m => m.Column("champion_id")));
        Set(x => x.FeaturedStories,
            m =>
            {
                m.Table("champion_story");
                m.Cascade(Cascade.None);
                m.Key(k => k.Column("story_id"));
            },
            r => 
                r.ManyToMany(m => m.Column("champion_id")));
        Set(x => x.FeaturedComic,
            m =>
            {
                m.Table("champion_comic");
                m.Cascade(Cascade.None);
                m.Key(k => k.Column("comic_id"));
            },
            r => 
                r.ManyToMany(m => m.Column("champion_id")));

        
        
        Set(x => x.Skins,
            m => m.Key(k => k.Column("champion_id")),
            r => r.OneToMany());
        Set(x => x.RelatedChampions, 
            m => m.Key(k => k.Column("related_champion_id")),
            r => r.OneToMany());
        Set(x => x.Abilities,
            m => m.Key(k => k.Column("champion_id")),
            r => r.OneToMany());
    }
}