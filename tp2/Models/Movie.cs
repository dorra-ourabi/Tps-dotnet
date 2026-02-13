
using System;
using System.ComponentModel.DataAnnotations;       // For [Key], [Required], [MaxLength]
using System.ComponentModel.DataAnnotations.Schema;
namespace tp2.Models;
public class Movie
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid(); 
    public string ? Name { get ; set ; }
    public Guid GenreId { get; set; }
    [ForeignKey("GenreId")]
    public Genre Genre { get; set; } = null!;
}