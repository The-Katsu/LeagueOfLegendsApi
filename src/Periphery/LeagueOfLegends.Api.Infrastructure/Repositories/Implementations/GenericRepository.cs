using LeagueOfLegends.Api.Domain.Entities.Base;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;

public abstract class GenericRepository<T> : IRepository<T> where T : Entity 
{
    protected INHibernateDbContext DbContext { get; }

    protected GenericRepository(INHibernateDbContext dbContext) => DbContext = dbContext;

    public IQueryable<T> ProvideQueryable() => DbContext.Query<T>();

    public virtual async Task<IList<T>> GetListAsync() => await DbContext.Query<T>().ToListAsync();
    public virtual async Task<T> GetByIdAsync(int id) => await DbContext.GetByIdAsync<T>(id);
}