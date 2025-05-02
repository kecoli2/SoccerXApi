using Microsoft.AspNetCore.Localization;
using SoccerX.API.HostedService;
using SoccerX.API.Middleware;
using SoccerX.API.StartUp;
using SoccerX.Common.Configuration;
using System.Globalization;
using Quartz.Logging;
using SoccerX.Infrastructure.Jobs.Base;
using SoccerX.Common.Constants;

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
builder.Services.AddDependcyCollectionWebApi(applicationSettings);
builder.Services.AddHostedService<QuartzHostedService>();
// Add services to the container.

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("tr-TR"),
        new CultureInfo("en-US")
    };

    options.DefaultRequestCulture = new RequestCulture("tr-TR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.ApplyCurrentCultureToResponseHeaders = true;

    options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
    {
        var acceptLanguage = context.Request.GetTypedHeaders().AcceptLanguage;
        var requestedCulture = acceptLanguage?.FirstOrDefault()?.Value.Value;

        if (string.IsNullOrWhiteSpace(requestedCulture))
        {
            requestedCulture = "tr-TR"; // Default Culture
        }

        var supportedCultures = new List<CultureInfo> { new("en-US"), new("tr-TR") };

        var culture = supportedCultures.Any(c =>
            c.Name.Equals(requestedCulture, StringComparison.OrdinalIgnoreCase))
            ? requestedCulture
            : "tr-TR";

        return Task.FromResult(new ProviderCultureResult(culture, culture));
    }));
});
LogProvider.SetCurrentLogProvider(new ConsoleLogProviderQuartz());

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Policy Tanimlari
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(SoccerXConstants.PolicySoccerX, policy =>
    {
        policy.RequireRole(SoccerXConstants.RoleAdmin);
        policy.RequireRole(SoccerXConstants.RoleEditor);
        policy.RequireRole(SoccerXConstants.RoleUser);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = ""; // Uygulama köküne koyar (örn: https://localhost:5001)
        c.ConfigObject.AdditionalItems["headers"] = new[] { "Accept-Language: tr-TR" };
    });

    app.MapOpenApi();
}

//MiddleWare
app.UseRouting();
app.UseRequestLocalization();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<TokenRefreshMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();