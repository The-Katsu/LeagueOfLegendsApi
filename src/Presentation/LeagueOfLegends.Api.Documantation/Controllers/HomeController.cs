using System.Diagnostics;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using LeagueOfLegends.Api.Documantation.Models;

namespace LeagueOfLegends.Api.Documantation.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;

    public HomeController(IHttpClientFactory clientFactory) => 
        _httpClient = clientFactory.CreateClient("leagueoflegendsapi");

    private readonly IList<string> _favoriteChampions = new List<string>
    {
        "Fiddlesticks",
        "Jinx",
        "Yasuo",
        "Akali"
    };
    
    private readonly IList<string> _favoriteRegions = new List<string>
    {
        "The Void",
        "Shurima",
        "Zaun",
        "The Freljord"
    };
    
    private readonly IList<string> _favoriteVideos = new List<string>
    {
        "Aurelion Sol: The Star Forger Returns",
        "Tales of the Black Mist: The Harrowing",
        "Mind of the Virtuoso",
        "League of Legends Music: Get Jinxed"
    };

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("api/champion");
        var championsContent = await response.Content.ReadFromJsonAsync<Response<Champion>>();
        ViewBag.Champions = championsContent.Results.Where(x => _favoriteChampions.Contains(x.Name)).ToList();
        response = await _httpClient.GetAsync("api/region");
        var regionsContent = await response.Content.ReadFromJsonAsync<Response<Region>>();
        ViewBag.Regions = regionsContent.Results.Where(x => _favoriteRegions.Contains(x.Name)).ToList();
        response = await _httpClient.GetAsync("api/video");
        var videosContent = await response.Content.ReadFromJsonAsync<Response<Video>>();
        foreach (var video in videosContent?.Results)
            video.Url = video.Url?.Replace("https://www.youtube.com/watch?v=", string.Empty);
        ViewBag.Videos = videosContent.Results.Where(x => _favoriteVideos.Contains(x.Title)).ToList();
        
        ViewBag.StoryUrl = _httpClient.BaseAddress.AbsoluteUri + "api/story/";
        ViewBag.ComicUrl = _httpClient.BaseAddress.AbsoluteUri + "api/comic/";
        ViewBag.ChampionUrl = _httpClient.BaseAddress.AbsoluteUri + "api/champion/";
        ViewBag.RegionUrl = _httpClient.BaseAddress.AbsoluteUri + "api/region/";
        ViewBag.VideoUrl = _httpClient.BaseAddress.AbsoluteUri + "api/video/";
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Documentation()
    {
        var url = _httpClient.BaseAddress.AbsoluteUri;
        ViewBag.BaseUrl = url.Remove(url.Length - 1);
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}