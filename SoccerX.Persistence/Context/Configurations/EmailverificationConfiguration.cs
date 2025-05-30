﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoccerX.Domain.Entities;
using SoccerX.Persistence.Context;
using System;
using System.Collections.Generic;

namespace SoccerX.Persistence.Context.Configurations
{
    public partial class EmailverificationConfiguration : IEntityTypeConfiguration<Emailverification>
    {
        public void Configure(EntityTypeBuilder<Emailverification> entity)
        {
            entity.HasKey(e => e.Id).HasName("emailverifications_pkey");

            entity.ToTable("emailverifications");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(6)
                .HasColumnName("code");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Expiresat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiresat");
            entity.Property(e => e.Isused)
                .HasDefaultValue(false)
                .HasColumnName("isused");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Emailverifications)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("fk_user_emailverification");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Emailverification> entity);
    }
}
