namespace LeagueOfLegends.Api.Application.Contracts.Responses;

public class ArrayResponse<T> where T : class
{
    public IList<T> Results { get; init; } = new List<T>();
}