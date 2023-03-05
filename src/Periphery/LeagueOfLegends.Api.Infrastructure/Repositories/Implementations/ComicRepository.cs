using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;

public class ComicRepository : GenericRepository<Comic>, IComicRepository
{
    public ComicRepository(INHibernateDbContext dbContext) : base(dbContext)
    {
    }
}