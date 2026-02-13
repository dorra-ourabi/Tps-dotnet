using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp4.Models;

public class MembershipTypes
{
    [Key]
    
    public int Id { get; set; }
    public int SignUpFee { get; set; }
    public int DurationInMonth  { get; set; }
    public int DiscountRate { get; set; }
    public virtual  ICollection<Customers> customers { get; set; }= new List<Customers>();
}