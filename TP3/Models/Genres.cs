using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TP3.Models;

public class Genres
{   [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string GenreName { get; set;  }
    public virtual ICollection<Movies> movies { get; set; }= new List<Movies>();
}