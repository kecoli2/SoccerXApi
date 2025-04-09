using Microsoft.IdentityModel.Tokens;
using SoccerX.API.HostedService;
using SoccerX.API.Middleware;
using SoccerX.API.StartUp;
using SoccerX.Common.Configuration;

var builder = WebApplication.CreateBuilder(args);

//settings
var applicationSettings = new ApplicationSettings
{

    Quartz = new QuartzSettings
    {
        Enabled = true,
        JobStoreType = "Database",
        MaxConcurrency = 5,
        MisfireThreshold = 60000,
        StartOnStartup = true,
        TriggerCheckInterval = 10
    },

    RateLimit = new RateLimitSettings
    {
        Limit = 100,
        Enabled = true,
        BlockOnLimit = true,
        GlobalLimit = false,
        LimitExceededMessage = "Rate limit exceeded",
        PeriodInSeconds = 60,
    },

    Redis = new RedisSettings
    {
        Database = 0,
        Host = "localhost",
        MasterName = "mymaster",
        Password = "",
        Port = 6379,
        SentinelHosts = Array.Empty<string>(),
        UseSentinel = false,
        UseSsl = false
    },

    JwtSettings = new JwtSettings
    {
        SecretKey = "6r7uF7QZyhivcEWnlweJ2m8YikxjS4Xx6BmUxBh/ax8q64tKgplooNKBppJc9ntM"
    }
    
};

//DI Tanimlamalari
builder.Services.AddHostedService<QuartzHostedService>();
builder.Services.AddDependcyCollectionWebApi(applicationSettings);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = ""; // Uygulama köküne koyar (örn: https://localhost:5001)
    });
    
    app.MapOpenApi();
}

//MiddleWare
app.UseMiddleware<TokenRefreshMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();