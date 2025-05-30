﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using System;
using System.Collections.Generic;

namespace SoccerX.Persistence.Context.Configurations
{
    public partial class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> entity)
        {
            entity.HasKey(e => e.Id).HasName("countries_pkey");

            entity.ToTable("countries");

            entity.HasIndex(e => e.Countrycode, "uq_countries_countrycode").IsUnique();

            entity.HasIndex(e => e.Name, "uq_countries_name").IsUnique();

            entity.HasIndex(e => new { e.Name, e.Countrycode }, "uq_countries_name_countrycode").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Countrycode)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("countrycode");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Phonemask)
                .HasMaxLength(100)
                .HasColumnName("phonemask");
            entity.Property(e => e.Phoneregex)
                .HasMaxLength(100)
                .HasColumnName("phoneregex");
            entity.Property(e => e.Updatedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedate");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Country> entity);
    }
}
