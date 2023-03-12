namespace LeagueOfLegends.Api.Documantation.Models;

public class Response<T>
{
    public List<T> Results { get; set; } = new List<T>();
}

