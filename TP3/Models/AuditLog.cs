using System;
using System.ComponentModel.DataAnnotations;

namespace TP3.Models
{
    public class AuditLog
    {
        [Key]
        public int Id { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; } // "Added", "Modified", "Deleted"
        public string EntityKey { get; set; }
        public string? Changes { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}