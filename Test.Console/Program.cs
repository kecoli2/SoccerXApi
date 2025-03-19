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
    var users = new Users
    {
        Id = Guid.NewGuid(),
        Username = "johndoe",
        Email = "45646546",
        Passwordhash = "ddasdasdsada",
        Status = UserStatus.Banned,
        Role = UserRole.Editor
    };


    context.Users.Add(users);
    context.SaveChanges();
}



Console.WriteLine("Hello, World!");
