// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Npgsql;
using Npgsql.NameTranslation;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;
using SoccerX.Infrastructure;
using SoccerX.Infrastructure.Util;
using SoccerX.Persistence.Context;
using SoccerX.Persistence.Util;
using System;


var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host=localhost;Port=5432;Database=SoccerXDB;Username=postgres;Password=kecoli2");
dataSourceBuilder.NpgsqlToEnumMapRegisterAll();
var dataSource = dataSourceBuilder.Build();
var optionsBuilder = new DbContextOptionsBuilder<SoccerXDbContext>();
optionsBuilder.UseNpgsql(dataSource, o=> o.NpgsqlToEnumMapRegisterAll());

using (var context = new SoccerXDbContext(optionsBuilder.Options))
{
    //var users = new List<Users>();
    //var usersFirst = new Users
    //{
    //    Id = Guid.NewGuid(),
    //    Username = "johndoe",
    //    Email = "45646546",
    //    Passwordhash = "ddasdasdsada",
    //    Status = UserStatus.Active,
    //    Role = UserRole.Editor
    //};

    //var usersSecond = new Users
    //{
    //    Id = Guid.NewGuid(),
    //    Username = "johndoeadsdsds",
    //    Email = "45646546ddddddd",
    //    Passwordhash = "ddasdasdsada",
    //    Status = UserStatus.Active,
    //    Role = UserRole.Editor
    //};

    //users.Add(usersFirst);
    //users.Add(usersSecond);
    //context.Users.AddRange(users);

    var user = context.Users.Where(x => x.Username == "johndoe").First();

    var user2 = context.Users.Where(x => x.Username == "johndoeadsdsds").First();


    user.Blocked.Add(user2);

    context.Users.Update(user!);

    context.SaveChanges();
}



Console.WriteLine("Hello, World!");
