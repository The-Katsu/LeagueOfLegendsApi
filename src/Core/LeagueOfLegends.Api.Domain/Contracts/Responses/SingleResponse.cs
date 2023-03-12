namespace LeagueOfLegends.Api.Domain.Contracts.Responses;

public class SingleResponse<T> where T : class
{
    public SingleResponse(T? result) => Result = result;

    public T? Result { get; set; }
}