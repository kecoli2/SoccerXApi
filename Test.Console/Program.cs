// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Npgsql;
using SoccerX.Domain.Entities;
using SoccerX.Domain.Enums;
using SoccerX.Infrastructure.Data;
using System;


var optionsBuilder = new DbContextOptionsBuilder<SoccerXDbContext>();
optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=SoccerXDB;Username=postgres;Password=kecoli2", o=> o.MapEnum<UserRole>());

using (var context = new SoccerXDbContext(optionsBuilder.Options))
{
    var users = new Users
    {
        Id = Guid.NewGuid(),
        Username = "johndoe",
        Email = "",
        Passwordhash = "ddasdasdsada",
        Role = UserRole.Admin,
    };


    context.Users.Add(users);
    context.SaveChanges();

}



    Console.WriteLine("Hello, World!");
