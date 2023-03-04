using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Domain.Entities.Base;
using NHibernate;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Data;

public interface INHibernateDbContext
{
    public void BeginTransaction();
    public Task Commit();
    public Task Rollback();
    public void CloseTransaction();
    public Task Save(Entity entity);
    public Task Delete(Entity entity);
    public IQuery CreateQuery(string sql);
    
    public IQueryable<Ability> Abilities { get; }
    public IQueryable<Champion> Champions { get; }
    public IQueryable<Comic> Comic { get; }
    public IQueryable<Race> Races { get; }
    public IQueryable<Region> Regions { get; }
    public IQueryable<Role> Roles { get; }
    public IQueryable<Skin> Skins { get; }
    public IQueryable<Story> Stories { get; }
    public IQueryable<Video> Videos { get; }
    public IQueryable<RelatedChampion> RelatedChampions { get; }
    public IQueryable<RelatedStory> RelatedStories { get; }
}