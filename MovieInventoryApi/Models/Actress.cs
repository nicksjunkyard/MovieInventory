using System.ComponentModel.DataAnnotations;

namespace MovieInventoryAPI.Models
{
    public class Actress
    {
        [Key]
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Biography { get; set; } = null;
        public bool IsNotActress { get; set; } = false;
        public bool IsCouple { get; set; } = false;
        public List<MovieActress> MovieActresses { get; } = [];

    }
}
