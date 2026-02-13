using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP3.Models;

public class Movies
{   
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } 
    public string Name { get; set; }
    public string? ImageFile { get; set; }
    public DateTime? DateAjoutMovie { get; set; }
    public int GenresId { get; set; }
    public virtual Genres Genre { get; set; }
    public virtual ICollection <Customers> clients { get;set; }=new List<Customers>();
}