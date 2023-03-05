﻿using LeagueOfLegends.Api.Persistence.NHibernate.Data;
using LeagueOfLegends.Api.Persistence.NHibernate.Migrations;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Configuration = NHibernate.Cfg.Configuration;

namespace LeagueOfLegends.Api.Persistence.NHibernate.Extensions;

public static class NHibernateExtensions
{
    public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
    {
        var mapper = new ModelMapper();
        mapper.AddMappings(typeof(NHibernateExtensions).Assembly.ExportedTypes);
        var hbmMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

        var configuration = new Configuration();
        configuration.DataBaseIntegration(p =>
        {
            p.Dialect<PostgreSQL82Dialect>();
            p.ConnectionString = connectionString;
            p.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
            p.SchemaAction = SchemaAutoAction.Validate;
            p.LogFormattedSql = false;
            p.LogSqlInConsole = false;
        });
        configuration.AddMapping(hbmMapping);

#if DEBUG
        if(!NHibernateMigrationsManager.IniCreated()) 
            new SchemaExport(configuration).Create(NHibernateMigrationsManager.InitMigration, true);
        else
            new SchemaUpdate(configuration).Execute(NHibernateMigrationsManager.UpdateMigration, true);
#endif
        
        var sessionFactory = configuration.BuildSessionFactory();

        services.AddSingleton(sessionFactory);
        services.AddScoped(_ => sessionFactory.OpenSession());
        services.AddScoped<INHibernateDbContext, NHibernateDbContext>();
        
        return services;
    }
}