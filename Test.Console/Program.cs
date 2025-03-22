// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Npgsql;
using SoccerX.Common.Configuration;
using SoccerX.Common.Extensions;
using SoccerX.Common.Helpers;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Util;

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
    }
};

var dataSourceBuilder = new NpgsqlDataSourceBuilder(applicationSettings.GetDatabaseConnectionString());
dataSourceBuilder.NpgsqlToEnumMapRegisterAll();
var dataSource = dataSourceBuilder.Build();
var optionsBuilder = new DbContextOptionsBuilder<SoccerXDbContext>();
optionsBuilder.UseNpgsql(dataSource, o =>
{
    o.NpgsqlToEnumMapRegisterAll();    
});


var jsonStrimgs = applicationSettings.ToJson().MinifyJson();
var jsonEncrypt = jsonStrimgs.Encrypt();

var jsonDecrypt = jsonEncrypt.Decrypt();


using (var context = new SoccerXDbContext(optionsBuilder.Options))
{
    var users = new List<Users>();
    var country = context.Countries.Take(1).First();
    var cities = context.Cities.Take(1).First();
    var usersFirst = new Users
    {
        Id = Guid.NewGuid(),
        Username = "johndoe",
        Email = "45646546",
        Passwordhash = "ddasdasdsada",
        Status = UserStatus.Active,
        Role = UserRole.Editor,
        Address = "asdasdasd",
        Phonenumber = "123123123123",
        Countryid = country.Id,
        Cityid = cities.Id
    };

    var usersSecond = new Users
    {
        Id = Guid.NewGuid(),
        Username = "johndoeadsdsds",
        Email = "45646546ddddddd",
        Passwordhash = "ddasdasdsada",
        Status = UserStatus.Active,
        Role = UserRole.Editor,
        Address = "asdasdasd",
        Phonenumber = "123123178123",
        Countryid = country.Id,
        Cityid = cities.Id
    };


    users.Add(usersFirst);
    users.Add(usersSecond);
    context.Users.AddRange(users);

    //var user = context.Users.Where(x => x.Username == "johndoe").FirstOrDefault();

    //var user2 = context.Users.Where(x => x.Username == "johndoeadsdsds").FirstOrDefault();


    //user.Blocked.Add(user2);

    //context.Users.Update(user!);

    context.SaveChanges();
}



Console.WriteLine("Hello, World!");
