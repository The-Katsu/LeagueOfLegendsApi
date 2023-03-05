using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using LeagueOfLegends.Api.Persistence.NHibernate.Data;

namespace LeagueOfLegends.Api.Infrastructure.Repositories.Implementations;

public class AbilityRepository  : GenericRepository<Ability>, IAbilityRepository
{
    public AbilityRepository(INHibernateDbContext dbContext) : base(dbContext)
    {
    }
}