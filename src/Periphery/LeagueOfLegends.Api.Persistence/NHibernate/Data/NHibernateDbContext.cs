using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Domain.Entities.Base;
using NHibernate;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Data;

public class NHibernateDbContext : INHibernateDbContext
{
    private readonly ISession _session;
    private ITransaction? _transaction;

    public NHibernateDbContext(ISession session) => _session = session;

    public IQueryable<Ability> Abilities => _session.Query<Ability>();
    public IQueryable<Champion> Champions => _session.Query<Champion>();
    public IQueryable<Comic> Comic => _session.Query<Comic>();
    public IQueryable<Race> Races => _session.Query<Race>();
    public IQueryable<Region> Regions => _session.Query<Region>();
    public IQueryable<Role> Roles => _session.Query<Role>();
    public IQueryable<Skin> Skins => _session.Query<Skin>();
    public IQueryable<Story> Stories => _session.Query<Story>();
    public IQueryable<Video> Videos => _session.Query<Video>();
    public IQueryable<RelatedChampion> RelatedChampions => _session.Query<RelatedChampion>();
    public IQueryable<RelatedStory> RelatedStories => _session.Query<RelatedStory>();
    
    public void BeginTransaction() => _transaction = _session.BeginTransaction();

    public async Task Commit() => await _transaction?.CommitAsync()!;

    public async Task Rollback() => await _transaction?.RollbackAsync()!;

    public async Task Save(Entity entity) => await _session.SaveOrUpdateAsync(entity);

    public async Task Delete(Entity entity) => await _session.DeleteAsync(entity);

    public void CloseTransaction()
    {
        if (_transaction is null) return;
        
        _transaction.Dispose();
        _transaction = null;
    }
}