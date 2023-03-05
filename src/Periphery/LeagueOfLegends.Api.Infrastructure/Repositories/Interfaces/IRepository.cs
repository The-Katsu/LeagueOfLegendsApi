using LeagueOfLegends.Api.Domain.Entities.Base;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;

public interface IRepository<T> where T : Entity
{
    public Task<IList<T>> GetListAsync();
    public Task<T> GetByIdAsync(Guid id);
}