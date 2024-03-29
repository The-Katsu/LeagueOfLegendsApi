﻿using LeagueOfLegends.Api.Persistence.NHibernate.Extensions;
using LeagueOfLegends.Api.Persistence.Quartz.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeagueOfLegends.Api.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("LolDbPgsqlConnection");
        services.AddNHibernate(connectionString!);
        services.AddQuartz();   
        return services;
    }
}