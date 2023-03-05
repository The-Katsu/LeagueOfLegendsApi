using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;

public class SkinRepository : GenericRepository<Skin>, ISkinRepository
{
    public SkinRepository(INHibernateDbContext dbContext) : base(dbContext)
    {
    }
}