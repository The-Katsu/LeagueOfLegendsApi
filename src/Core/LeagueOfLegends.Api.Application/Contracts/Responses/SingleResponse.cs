namespace LeagueOfLegends.Api.Application.Contracts.Responses;

public class SingleResponse<T> where T : class
{
    public T? Result { get; set; }
}