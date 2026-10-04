using System.ComponentModel.DataAnnotations;

namespace MovieInventoryAPI.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Biography { get; set; } = null;
        public bool IsNotActor { get; set; } = false;
        public List<MovieActor> MovieActors { get; } = [];
    }
}
