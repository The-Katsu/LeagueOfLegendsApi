using System.Text.Json;

namespace LeagueOfLegends.Api.Domain.Errors;

public class HttpErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}