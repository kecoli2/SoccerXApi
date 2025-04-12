using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SoccerX.Common.Attributes;
using SoccerX.Domain.Enums;

namespace SoccerX.Persistence.Interceptors
{
    public class AuditSaveChangesInterceptor: SaveChangesInterceptor
    {
        #region Field
        
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            AddAuditLogs(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            AddAuditLogs(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void AddAuditLogs(DbContext? context)
        {
            if (context == null) return;

            var modifiedEntries = context.ChangeTracker.Entries()
                .Where(x => x.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .ToList();

            //foreach (var auditLog in from entry in modifiedEntries where IsAuditableEntity(entry.Entity) select new Auditlog
            //         {
            //             Entityname = entry.Entity.GetType().Name,
            //             Entityid = GetEntityId(entry),
            //             Action = ConvertAction(entry),
            //             Details = SerializeChanges(entry),
            //             Performedby = Guid.NewGuid(), // Güncel kullanıcı bilgisini ekleyin
            //             Timestamp = DateTime.UtcNow,
            //             Isdeleted = false
            //         })
            //{
            //    context.Set<Auditlog>().Add(auditLog);
            //}
        }

        private AuditAction ConvertAction(EntityEntry entry)
        {
            return entry.State switch
            {
                EntityState.Deleted => AuditAction.Delete,
                EntityState.Modified => AuditAction.Update,
                EntityState.Added => AuditAction.Create,
                _ => AuditAction.Delete
            };
        }

        private Guid GetEntityId(EntityEntry entry)
        {
            var pkProperty = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey());
            if (pkProperty?.CurrentValue == null)
            {
                return Guid.NewGuid();
            }

            // TryParse ile güvenli şekilde parse edelim
            return Guid.TryParse(pkProperty.CurrentValue.ToString(), out Guid parsedGuid) ? parsedGuid : Guid.NewGuid();
        }

        private bool IsAuditableEntity(object entity)
        {
            // Entity'nin türünde [Auditable] attribute'u var mı kontrol ediyoruz.
            // Eğer çok sayıda entity kontrol edilecekse, bu bilgiyi cache'leyebilirsiniz.
            return entity.GetType().GetCustomAttributes(typeof(AuditLogTableAttributes), true).Any();
        }

        private string SerializeChanges(EntityEntry entry)
        {
            var changes = entry.Properties
                .Select(p => $"{p.Metadata.Name}: {p.OriginalValue} -> {p.CurrentValue}")
                .ToArray();
            return string.Join("; ", changes);
        }
        #endregion

        #region Private Method
        #endregion
    }
}
