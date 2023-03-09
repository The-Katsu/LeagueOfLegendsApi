using LeagueOfLegends.Api.Application;
using LeagueOfLegends.Api.Application.Jobs;
using LeagueOfLegends.Api.Infrastructure;
using LeagueOfLegends.Api.Persistence;
using LeagueOfLegends.Api.Persistence.Quartz.JobProviders;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services
    .AddPersistence(configuration)
    .AddInfrastructure(configuration)
    .AddApplication(configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var schedulerFactory = scope.ServiceProvider.GetService<ISchedulerFactory>();
    var scheduler = await schedulerFactory!.GetScheduler();
    await scheduler.ProvideCronJob<CrawlerJob>
        ("0 0 0 ? * * *"); // At 00:00:00am every day
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
