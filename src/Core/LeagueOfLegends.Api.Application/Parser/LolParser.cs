using LeagueOfLegends.Api.Application.Mappers;
using LeagueOfLegends.Api.Application.Parser.Constants;
using LeagueOfLegends.Api.Application.Parser.Extensions;
using LeagueOfLegends.Api.Domain.Entities;
using LeagueOfLegends.Api.Infrastructure;
using LeagueOfLegends.Api.Infrastructure.Repositories.Interfaces;
using Newtonsoft.Json.Linq;

namespace LeagueOfLegends.Api.Application.Parser;

public class LolParser : ILolParser
{
    private readonly HttpClient _httpClient;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;
    private readonly IRaceRepository _raceRepository;
    private readonly IChampionRepository _championRepository;
    
    public LolParser(HttpClient httpClient, 
        IUnitOfWork unitOfWork)
    {
        _httpClient = httpClient;
        _unitOfWork = unitOfWork;
        _roleRepository = _unitOfWork.RoleRepository;
        _raceRepository = _unitOfWork.RaceRepository;
        _championRepository = _unitOfWork.ChampionRepository;
    }
    
    public async Task ParseChampions()
    {
        Console.WriteLine("executed");
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

        var abilitiesDictionary = new Dictionary<string, List<Ability>>(); // <champ name, abilities>
        foreach (var (key, value) in championsData)
        {
            var passive = JTokenMapper.MapPassiveAbility(value.gameJObject);
            var q = JTokenMapper.MapQAbility(value.gameJObject);
            var w = JTokenMapper.MapWAbility(value.gameJObject);
            var e = JTokenMapper.MapEAbility(value.gameJObject);
            var r = JTokenMapper.MapRAbility(value.gameJObject);
            abilitiesDictionary.Add(
                key, 
                new List<Ability>());
            if (passive.Name is not null) abilitiesDictionary[key].Add(passive);
            if (q.Name is not null) abilitiesDictionary[key].Add(q);
            if (w.Name is not null) abilitiesDictionary[key].Add(w);
            if (e.Name is not null) abilitiesDictionary[key].Add(e);
            if (r.Name is not null) abilitiesDictionary[key].Add(r);
        }

        var skinsDictionary = new Dictionary<string, List<Skin>>();
        foreach (var (key, value) in championsData)
        {
            var skinTokens = value.gameJObject.GetChampionSkins();
            var skins = new List<Skin>();
            if (skinTokens is not null) 
                skins.AddRange(skinTokens.Select(JTokenMapper.MapSkin));
            skinsDictionary.Add(key, skins);
        }

        var raceDictionary = new Dictionary<string, List<Race>>();
        var allRaces = new List<Race>();
        foreach (var (key, value) in championsData)
        {
            var raceTokens = value.universeJObject.GetChampionRaces();
            var races = new List<Race>();
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
            var roleTokens = value.universeJObject.GetChampionRoles();
            var roles = new List<Role>();
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
    }
}