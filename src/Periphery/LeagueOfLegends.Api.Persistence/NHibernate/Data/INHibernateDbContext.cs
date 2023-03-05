using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Domain.Entities.Base;
using NHibernate;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Data;

public interface INHibernateDbContext
{
    public void BeginTransaction();
    public Task Commit(CancellationToken token);
    public Task RollbackAsync(CancellationToken token = default);
    public void CloseTransaction();
    public Task SaveAsync(Entity entity, CancellationToken token = default);
    public Task DeleteAsync(Entity entity, CancellationToken token = default);

    public IQueryable<T> Query<T>() where T : Entity;
    public IQueryOver<T, T> QueryOver<T>() where T : Entity;
    public Task<T> GetByIdAsync<T>(Guid id) where T : Entity;
}