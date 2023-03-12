using System.Reflection;

namespace LeagueOfLegends.Api.Domain.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(MemberInfo entityType, int id) :
        base($"{entityType.Name} with id {id} does not exist.") { }
}