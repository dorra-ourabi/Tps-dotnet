using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp4.Models;

public class Customers
{
    
    public int  Id { get; set; }
    public string Name { get; set; }
    public int membershiptypesId { get; set; }
    public bool IsSubscribed { get; set; }
    public virtual MembershipTypes membershiptypes { get; set; }
    public virtual ICollection<Movies> movies { get; set; } = new List<Movies>();
}