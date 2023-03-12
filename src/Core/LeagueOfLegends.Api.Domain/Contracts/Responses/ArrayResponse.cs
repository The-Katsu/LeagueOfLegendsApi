namespace LeagueOfLegends.Api.Domain.Contracts.Responses;

public class ArrayResponse<T> where T : class
{
    public ArrayResponse(IList<T> results) => Results = results;

    public IList<T> Results { get; init; }
}