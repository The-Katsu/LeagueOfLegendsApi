using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;
using NHibernate.Linq;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(INHibernateDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Role> GetByNameAsync(string name) => 
        await DbContext.Query<Role>().FirstOrDefaultAsync(x => x.Name == name);
}