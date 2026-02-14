using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TP3.Models;

public class Membershiptypes
{   [Key]
    
    public int Id { get; set; }
    public int SignUpFee { get; set; }
    public int DurationInMonth  { get; set; }
    public int DiscountRate { get; set; }
    public virtual  ICollection<Customers> customers { get; set; }= new List<Customers>();
    
}