using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LeagueOfLegends.Api.Application.Parser.Extensions;

public static class HttpClientExtensions
{
    public static async Task<JObject> GetDataAsync(this HttpClient httpClient, Uri path)
    {
        var response = await httpClient.GetAsync(path);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<JObject>(content);
            return data!;
        }

        return new JObject();
    }
}