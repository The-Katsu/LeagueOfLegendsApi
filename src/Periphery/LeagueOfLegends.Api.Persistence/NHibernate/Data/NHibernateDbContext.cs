using LeagueOfLegends.Api.Domain.Entities.Base;
using NHibernate;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Data;

public class NHibernateDbContext : INHibernateDbContext
{
    private readonly ISession _session;
    private ITransaction? _transaction;

    public NHibernateDbContext(ISession session) => _session = session;

    public IQueryable<T> Query<T>() where T : Entity => _session.Query<T>();
    public IQueryOver<T, T> QueryOver<T>() where T : Entity => _session.QueryOver<T>(); 
    public async Task<T> GetByIdAsync<T>(int id) where T : Entity => await _session.GetAsync<T>(id);

    public void BeginTransaction() => 
        _transaction = _session.BeginTransaction();
    public async Task CommitAsync(CancellationToken token) => 
        await _transaction?.CommitAsync(token)!;

    public async Task RollbackAsync(CancellationToken token = default) => 
        await _transaction?.RollbackAsync(token)!;

    public async Task SaveOrUpdateAsync(Entity entity, CancellationToken token = default) => 
        await _session.SaveOrUpdateAsync(entity, token);

    public async Task DeleteAsync(Entity entity, CancellationToken token = default) => 
        await _session.DeleteAsync(entity, token);

    public void CloseTransaction()
    {
        if (_transaction is null) return;
        _session.Clear();
        _transaction.Dispose();
        _transaction = null;
    }
}