using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;

public class StoryRepository : GenericRepository<Story>, IStoryRepository
{
    public StoryRepository(INHibernateDbContext dbContext) : base(dbContext)
    {
    }
}