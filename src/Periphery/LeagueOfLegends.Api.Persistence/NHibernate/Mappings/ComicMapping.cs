﻿using LeagueOfLegends.Api.Domain.Entities;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Mappings;

public class ComicMapping : ClassMapping<Comic>
{
    public ComicMapping()
    {
        Table("comic");
        
        Id(x => x.Id, m =>
        {
            m.Generator(Generators.Guid);
            m.Type(NHibernateUtil.Guid);
            m.Column("id");
            m.UnsavedValue(Guid.Empty);
        });

        Property(x => x.Title, m => m.Column("title"));
        Property(x => x.Series, m => m.Column("series"));
        Property(x => x.Pages, m => m.Column("pages"));
        Property(x => x.ScaledPages, m => m.Column("scaled_pages"));
        Property(x => x.Credits, m => m.Column("credits"));
        Property(x => x.Description, m => m.Column("description"));
        
        Set(x => x.FeaturedChampions,
            m =>
            {
                m.Table("champion_comic");
                m.Cascade(Cascade.None);
                m.Key(k => k.Column("champion_id"));
            },
            r => 
                r.ManyToMany(m => m.Column("comic_id")));
    }
}