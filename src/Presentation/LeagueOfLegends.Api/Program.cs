var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

builder.Services
    .AddPersistence(configuration)
    .AddInfrastructure(configuration)
    .AddApplication(configuration);

var app = builder.Build();

await app.StartQuartzJobs();

app.UseAuthorization();
app.UseFastEndpoints(c => c.Endpoints.RoutePrefix = "api");
app.UseSwaggerGen();

app.Run();