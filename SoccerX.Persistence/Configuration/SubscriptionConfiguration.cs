using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SoccerX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Persistence.Configuration
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscriptions>
    {
        public void Configure(EntityTypeBuilder<Subscriptions> entity)
        {
            entity.ToTable("subscriptions");

            entity.HasKey(e => e.Id).HasName("subscriptions_pkey");
            entity.Property(e => e.Id).HasDefaultValueSql("gen_random_uuid()").HasColumnName("id");
            entity.Property(e => e.Editorid).HasColumnName("editorid");
            entity.Property(e => e.Subscriberid).HasColumnName("subscriberid");
            entity.Property(e => e.Startdate).HasColumnType("timestamp without time zone").HasColumnName("startdate");
            entity.Property(e => e.Enddate).HasColumnType("timestamp without time zone").HasColumnName("enddate");
            entity.Property(e => e.Isactive).HasDefaultValue(true).HasColumnName("isactive");
            entity.Property(e => e.Createdate).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("timestamp without time zone").HasColumnName("createdate");            
            entity.Property(e => e.Isdeleted).HasDefaultValue(false).HasColumnName("isdeleted");

            entity.HasOne(e => e.Editor).WithMany(u => u.SubscriptionsEditor).HasForeignKey(e => e.Editorid).HasConstraintName("subscriptions_editorid_fkey");
            entity.HasOne(e => e.Subscriber).WithMany(u => u.SubscriptionsSubscriber).HasForeignKey(e => e.Subscriberid).HasConstraintName("subscriptions_subscriberid_fkey");
        }
    }
}
