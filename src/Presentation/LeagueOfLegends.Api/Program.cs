var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();
builder.Services.AddGraphQl();

builder.Services
    .AddPersistence(configuration)
    .AddInfrastructure(configuration)
    .AddApplication(configuration);

var app = builder.Build();

await app.StartQuartzJobs();

app.UseMiddleware<GlobalExceptionHandler>();
app.UseAuthorization();
app.UseFastEndpoints(c => c.Endpoints.RoutePrefix = "api");
app.MapGraphQL();
app.UseSwaggerGen();


app.Run();