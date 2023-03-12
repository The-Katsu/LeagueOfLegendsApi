using System.Reflection;

namespace LeagueOfLegends.Api.Domain.Exceptions;

public class PageNotFoundException : Exception
{
    public PageNotFoundException(MemberInfo entityType, int page) : 
        base($"{entityType.Name} page number {page} have no items") { }
}