using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;

public class RegionRepository : GenericRepository<Region>, IRegionRepository
{
    public RegionRepository(INHibernateDbContext dbContext) : base(dbContext)
    {
    }
}