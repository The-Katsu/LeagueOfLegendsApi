using LeagueOfLegends.Api.Application.Jobs.Crawler.Parser.Constants;
using LeagueOfLegends.Api.Application.Jobs.Crawler.Parser.Extensions;
using LeagueOfLegends.Api.Application.Mappers;
using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using Newtonsoft.Json.Linq;

namespace LeagueOfLegends.Api.Application.Jobs.Crawler.Parser;

public class LolParser : ILolParser
{
    private readonly HttpClient _httpClient;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;
    private readonly IRaceRepository _raceRepository;
    private readonly IChampionRepository _championRepository;
    private readonly IVideoRepository _videoRepository;
    private readonly IComicRepository _comicRepository;
    private readonly IStoryRepository _storyRepository;
    private readonly IRegionRepository _regionRepository;
    
    public LolParser(HttpClient httpClient, 
        IUnitOfWork unitOfWork)
    {
        _httpClient = httpClient;
        _unitOfWork = unitOfWork;
        _roleRepository = _unitOfWork.RoleRepository;
        _raceRepository = _unitOfWork.RaceRepository;
        _championRepository = _unitOfWork.ChampionRepository;
        _videoRepository = _unitOfWork.VideoRepository;
        _comicRepository = _unitOfWork.ComicRepository;
        _storyRepository = _unitOfWork.StoryRepository;
        _regionRepository = _unitOfWork.RegionRepository;
    }
    
    public async Task ParseChampions()
    {
        var universeChampionsData = await _httpClient.GetDataAsync(Uris.AllUniverseChampionsUrl);
        var gameChampionsData = await _httpClient.GetDataAsync(Uris.AllGameChampionUrl);

        var gameChampions = gameChampionsData.GetGameChampions();
        var universeChampions = universeChampionsData.GetUniverseChampions();

        var universeNames = universeChampions
            .Select(champion => champion.UniverseChampionNameWithoutSeparators())
            .ToList();
        
        var dbChampionNames = await _championRepository.GetChampionNamesAsync();
        var dbChampionNamesWithoutSeparators = dbChampionNames
            .Select(championName => 
                new string(championName.Where(char.IsLetter).ToArray())).ToList();

        universeNames = universeNames
            .Except(dbChampionNamesWithoutSeparators)
            .ToList();
        
        if (universeNames.Count == 0) return;

        var universeChampionsLinks = universeChampions
            .ToDictionary(
                champion => champion.UniverseChampionNameWithoutSeparators(), 
                champion => champion.UniverseChampionUrlName());
        var gameChampionsLinks = gameChampions
            .ToDictionary(
                champion =>champion.GameChampionNameWithoutSeparators(), 
                champion => champion.GameChampionUrlName());

        var championsData = new Dictionary<string, (JToken universeJObject, JToken gameJObject)>();
        foreach (var name in universeNames)
        {
            var tmp = (new JObject().ToObject<JToken>(), new JObject().ToObject<JToken>());
            if(universeChampionsLinks.TryGetValue(name, out var uUrl))
                tmp.Item1 = (await _httpClient
                    .GetDataAsync(Uris.UniverseChampionUrl(uUrl)))
                    .GetUniverseChampionNodes();
            if(gameChampionsLinks.TryGetValue(name, out var gUrl))
                tmp.Item2 = (await _httpClient
                        .GetDataAsync(Uris.GameChampionUrl(gUrl)))
                    .GetGameChampionNodes();
            championsData.Add(name, tmp!);
        }

        var championsDictionary = new Dictionary<string, Champion>();
        foreach (var (key, value) in championsData)
            championsDictionary.Add(
                key,
                JTokenMapper.MapChampion(value.universeJObject));

        var abilitiesDictionary = new Dictionary<string, List<Ability>>(); 
        foreach (var (key, value) in championsData)
        {
            var passive = JTokenMapper.MapPassiveAbility(value.gameJObject);
            var q = JTokenMapper.MapQAbility(value.gameJObject);
            var w = JTokenMapper.MapWAbility(value.gameJObject);
            var e = JTokenMapper.MapEAbility(value.gameJObject);
            var r = JTokenMapper.MapRAbility(value.gameJObject);
            
            abilitiesDictionary.Add(key, new List<Ability>());
            
            if (passive.Name is not null) abilitiesDictionary[key].Add(passive);
            if (q.Name is not null) abilitiesDictionary[key].Add(q);
            if (w.Name is not null) abilitiesDictionary[key].Add(w);
            if (e.Name is not null) abilitiesDictionary[key].Add(e);
            if (r.Name is not null) abilitiesDictionary[key].Add(r);
        }

        var skinsDictionary = new Dictionary<string, List<Skin>>();
        foreach (var (key, value) in championsData)
        {
            var skinTokens = value.gameJObject["skins"];
            var skins = new List<Skin>();
            if (skinTokens is not null) 
                skins.AddRange(skinTokens.Select(JTokenMapper.MapSkin));
            skinsDictionary.Add(key, skins);
        }

        var raceDictionary = new Dictionary<string, List<Race>>();
        var allRaces = new List<Race>();
        foreach (var (key, value) in championsData)
        {
            var raceTokens = value.universeJObject["races"];
            var races = new List<Race>();
            if (raceTokens is not null) 
                races.AddRange(raceTokens.Select(JTokenMapper.MapRace));
            allRaces.AddRange(races);
            raceDictionary.Add(key, races);
        }

        var dbRaces = await _raceRepository.GetListAsync();
        var newRaces = allRaces
            .DistinctBy(x => x.Name)
            .ExceptBy(
                dbRaces.Select(x => x.Name),
                race => race.Name)
            .ToList();
        
        _unitOfWork.BeginTransaction();
        foreach (var race in newRaces) 
            await _unitOfWork.AddOrUpdateAsync(race);
        await _unitOfWork.CommitAsync();
        
        var roleDictionary = new Dictionary<string, List<Role>>();
        var allRoles = new List<Role>();
        foreach (var (key, value) in championsData)
        {
            var roleTokens = value.universeJObject["roles"];
            var roles = new List<Role>();
            if (roleTokens is not null) 
                roles.AddRange(roleTokens.Select(JTokenMapper.MapRole));
            allRoles.AddRange(roles);
            roleDictionary.Add(key, roles);
        }

        var dbRoles = await _roleRepository.GetListAsync();
        var newRoles = allRoles
            .DistinctBy(x => x.Name)
            .ExceptBy(
                dbRoles.Select(x => x.Name),
                role => role.Name)
            .ToList();
        
        _unitOfWork.BeginTransaction();
        foreach (var role in newRoles) 
            await _unitOfWork.AddOrUpdateAsync(role);
        await _unitOfWork.CommitAsync();
        
        
        foreach (var (key, value) in championsDictionary)
        {
            _unitOfWork.BeginTransaction();
            
            var skins = skinsDictionary[key].ToHashSet();
            foreach (var skin in skins)
                await _unitOfWork.AddOrUpdateAsync(skin);
            value.Skins = skins;
            
            var abilities = abilitiesDictionary[key].ToHashSet();
            foreach (var ability in abilities) 
                await _unitOfWork.AddOrUpdateAsync(ability);
            value.Abilities = abilities;
            
            foreach (var role in roleDictionary[key])
                value.Roles.Add(
                    await _roleRepository.GetByNameAsync(role.Name));
            
            foreach (var race in raceDictionary[key])
                value.Races.Add(
                    await _raceRepository.GetByNameAsync(race.Name));
            
            await _unitOfWork.AddOrUpdateAsync(value);
            await _unitOfWork.CommitAsync();
        }

        var relatedChampionsDictionary = new Dictionary<string, List<string>>();
        foreach (var (key, value) in championsData)
        {
            var relatedChampionsToken = value.universeJObject.GetRelatedChampions();
            var names = relatedChampionsToken.Select(token => token.GetRelatedChampionName()).ToList();
            relatedChampionsDictionary.Add(key, names);
        }
        foreach (var (key, value) in championsDictionary)
        {
            _unitOfWork.BeginTransaction();
            var champion = await _championRepository.GetByNameAsync(value.Name);
            foreach (var name in relatedChampionsDictionary[key])
            {
                var relatedChampion = new RelatedChampion {Champion = await _championRepository.GetByNameAsync(name)};
                champion.RelatedChampions.Add(relatedChampion);
            }
            await _unitOfWork.AddOrUpdateAsync(champion);
            await _unitOfWork.CommitAsync();
        }
    }

    public async Task ParseUniverse()
    {
        var universeData = await _httpClient.GetDataAsync(Uris.ExploreUniverseUrl);
        var universeDictionary = new Dictionary<string, List<JToken>>
        {
            {ExploreItems.Video, new List<JToken>()},
            {ExploreItems.Comics, new List<JToken>()},
            {ExploreItems.Story, new List<JToken>()}
        };
        
        var bdVideos = await _videoRepository.GetTitlesAsync();
        var bdComic = await _comicRepository.GetTitlesAsync();
        var bdStories = await _storyRepository.GetTitlesAsync();

        var modules = universeData.GetUniverseExploreModules();
        foreach (var module in modules)
        {
            var type = module.GetExploreItemType();
            switch (type)
            {
                case ExploreItems.Video:
                    if (!bdVideos.Contains(module["title"]!.GetStringValue()))
                        universeDictionary[type].Add(module);
                    break;
                case ExploreItems.Story:
                    if (!bdStories.Contains(module["title"]!.GetStringValue()))
                        universeDictionary[type].Add(module);
                    break;
                case ExploreItems.Comics:
                    if (!bdComic.Contains(module["title"]!.GetStringValue()))
                        universeDictionary[type].Add(module);
                    break;
            }

        }
        
        var comicTokens = universeDictionary[ExploreItems.Comics];
        foreach (var token in comicTokens)
        {
            _unitOfWork.BeginTransaction();
            var str = token["url"]!.GetStringValue();
            var place = str.IndexOf("comic/", StringComparison.Ordinal);
            str = str.Remove(place, "comic/".Length);
            var url = Uris.ComicUri(str);
            var comicToken = await _httpClient.GetDataAsync(url);
            var comic = JTokenMapper.MapComic(token);
            var credits = comicToken["credits"]!;
            foreach (var credit in credits)
                comic.Credits += 
                    $"{credit["credit-label"]!.GetStringValue()} : {credit["credit-info"]!.GetStringValue()}\n";
            
            var pages = comicToken["desktop-pages"]!;
            foreach (var page in pages)
            foreach (var value in page)
                comic.Content += value["uri"]!.GetStringValue() + "\n";
            var champions = token["featured-champions"];

            foreach (var champ in champions)
            {
                var champion = await _championRepository.GetByNameAsync(champ["name"]!.GetStringValue());
                if (champion is not null)
                    comic.FeaturedChampions.Add(champion);
            }

            comic.Credits = string.Join(
                '\n',
                comic.Credits.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToList());
            
            comic.Content = string.Join(
                '\n',
                comic.Content.Split('\n').Where(x => !string.IsNullOrWhiteSpace(x)).ToList());
            
            await _unitOfWork.AddOrUpdateAsync(comic);
            await _unitOfWork.CommitAsync();
        }

        var storyTokens = universeDictionary[ExploreItems.Story];
        foreach (var token in storyTokens)
        {
            _unitOfWork.BeginTransaction();
            
            var storyToken = await _httpClient.GetDataAsync(Uris.ExplorerItemUri(token["url"]!.GetStringValue()));
            var story = JTokenMapper.MapStory(token, storyToken);
            foreach (var storySection in storyToken["story"]!["story-sections"]!.First!["story-subsections"]!)
                story.Content += storySection["content"]!.GetStringValue();

            foreach (var championToken in token["featured-champions"]!)
            {
                var champion = await _championRepository.GetByNameAsync(championToken["name"]!.GetStringValue());
                if (champion is not null)
                    story.FeaturedChampions.Add(champion);
            }
            
            await _unitOfWork.AddOrUpdateAsync(story);
            await _unitOfWork.CommitAsync();
        }
        
        var videoTokens = universeDictionary[ExploreItems.Video];
        foreach (var token in videoTokens)
        {
            _unitOfWork.BeginTransaction();
            
            var video = JTokenMapper.MapVideo(token);
            var featuredChampionsTokens = token["featured-champions"]!;

            foreach (var championsToken in featuredChampionsTokens)
            {
                var champion = await _championRepository.GetByNameAsync(championsToken["name"]!.GetStringValue());
                if (champion is not null)
                    video.FeaturedChampions.Add(champion);
            }
            
            await _unitOfWork.AddOrUpdateAsync(video);
            await _unitOfWork.CommitAsync();
        }
    }

    public async Task ParseRegions()
    {
        var regionsData = await _httpClient.GetDataAsync(Uris.RegionsUri);
        var regionTokens = regionsData["factions"]!;
        var dbRegions = await _regionRepository.GetNamesAsync();
        var slugs = new List<string>();
        foreach (var token in regionTokens)
            if (!dbRegions.Contains(token["name"]!.GetStringValue()))
                slugs.Add(token["slug"]!.GetStringValue());
        
        foreach (var slug in slugs)
        {
            _unitOfWork.BeginTransaction();

            var regionToken = await _httpClient.GetDataAsync(Uris.RegionUri(slug));
            var faction = regionToken["faction"]!;
            var region = JTokenMapper.MapRegion(faction);

            var champions = regionToken["associated-champions"]!;
            foreach (var championToken in champions)
            {
                var champion = await _championRepository.GetByNameAsync(championToken["name"]!.GetStringValue());
                if (champion is not null) 
                    region.AssociatedChampions.Add(champion);
            }

            await _unitOfWork.AddOrUpdateAsync(region);
            await _unitOfWork.CommitAsync();
        }

    }
}