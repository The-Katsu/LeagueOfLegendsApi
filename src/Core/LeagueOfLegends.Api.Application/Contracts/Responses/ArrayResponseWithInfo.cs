namespace LeagueOfLegends.Api.Application.Contracts.Responses;

public class ArrayResponseWithInfo<T> where T : class
{
    public Information Info { get; set; } = null!;
    public IList<T> Results { get; set; } = new List<T>();
}