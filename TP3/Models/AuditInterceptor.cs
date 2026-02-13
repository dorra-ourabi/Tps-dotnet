using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TP3.Models;

namespace TP3.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        // Cette méthode s'exécute juste avant l'enregistrement final en base
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            var context = eventData.Context;
            if (context == null) return base.SavingChanges(eventData, result);

            // On récupère toutes les entités modifiées, ajoutées ou supprimées
            // Sauf la table AuditLog elle-même pour éviter une boucle infinie
            var entries = context.ChangeTracker.Entries()
                .Where(e => e.Entity is not AuditLog && 
                            (e.State == EntityState.Added || 
                             e.State == EntityState.Modified || 
                             e.State == EntityState.Deleted))
                .ToList();

            foreach (var entry in entries)
            {
                var auditLog = new AuditLog
                {
                    TableName = entry.Entity.GetType().Name,
                    Action = entry.State.ToString(),
                    Date = DateTime.Now,
                    // On récupère la clé primaire de l'objet
                    EntityKey = entry.Properties.FirstOrDefault(p => p.Metadata.IsPrimaryKey())?.CurrentValue?.ToString() ?? "N/A",
                    Changes = entry.State == EntityState.Modified ? "Modification effectuée" : null
                };

                // On ajoute le log au contexte
                context.Add(auditLog);
            }

            return base.SavingChanges(eventData, result);
        }
    }
}