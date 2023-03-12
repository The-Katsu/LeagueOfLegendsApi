using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;
using NHibernate.Linq;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;

public class RaceRepository : GenericRepository<Race>, IRaceRepository
{
    public RaceRepository(INHibernateDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Race> GetByNameAsync(string name) => 
        await DbContext.Query<Race>().FirstOrDefaultAsync(x => x.Name == name);
}