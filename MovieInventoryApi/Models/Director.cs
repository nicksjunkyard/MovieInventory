using System.ComponentModel.DataAnnotations;

namespace MovieInventoryAPI.Models
{
    public class Director
    {
        [Key]
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public List<MovieDirector> MovieDirectors { get; } = [];
    }
}