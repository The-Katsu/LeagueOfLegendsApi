using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LeagueOfLegends.Api.Application.Jobs.Crawler.Parser.Extensions;

public static class HttpClientExtensions
{
    public static async Task<JObject> GetDataAsync(this HttpClient httpClient, Uri path)
    {
        var response = await httpClient.GetAsync(path);
        if (!response.IsSuccessStatusCode) return new JObject();
        
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<JObject>(content);
        return data!;
    }
}