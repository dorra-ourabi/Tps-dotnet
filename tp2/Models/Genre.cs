
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;              // For ICollection<T>

namespace tp2.Models
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); // MySQL -> char(36)

        [Required]
        [MaxLength(255)] // MySQL -> varchar(255)
        public string Name { get; set; } = null!;
        public ICollection<Movie> Movies { get; set; } = new List<Movie>();
    }
}