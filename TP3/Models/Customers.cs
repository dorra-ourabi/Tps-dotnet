using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP3.Models;

public class Customers
{   [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int  Id { get; set; }
    public string Name { get; set; }
    public int membershiptypesId { get; set; }
    public virtual Membershiptypes membershiptypes { get; set; }
    public virtual ICollection<Movies> movies { get; set; } = new List<Movies>();
}