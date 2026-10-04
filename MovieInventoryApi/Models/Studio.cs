
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;

namespace MovieInventoryAPI.Models
{
    public class Studio
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string URL { get; set; } = string.Empty;
        public ICollection<Movie> Movies { get; } = [];
    }
}
