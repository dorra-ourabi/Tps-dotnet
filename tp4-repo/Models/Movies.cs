using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tp4.Models;

public class Movies
{
    
    public int Id { get; set; } 
    public string Name { get; set; }
    public string? ImageFile { get; set; }
    public DateTime? DateAjoutMovie { get; set; }
    public int Stock { get; set; }
    public int GenresId { get; set; }
    public virtual Genre Genre { get; set; }
    public virtual ICollection <Customers> clients { get;set; }=new List<Customers>();
}