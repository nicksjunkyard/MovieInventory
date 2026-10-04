using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;

namespace MovieInventoryAPI.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string FileName { get; set; }
        public required string Description { get; set; } = string.Empty;
        public DateOnly ReleaseDate { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
        public DateTime? DateModified { get; set; } = DateTime.UtcNow;
        public string ApiCode { get; set; } = string.Empty;
        public int? StudioId { get; set; }
        public Studio? Studio { get; set; }
        public List<MovieDirector> MovieDirectors { get; } = [];
        public List<Director> Directors { get;} = [];
        public List<MovieActor> MovieActors { get; } = [];
        public List<Actor> Actors { get; } = [];
        public List<MovieActress> MovieActresses { get; } = [];
        public List<Actress> Actresses { get; } = [];
        public List<MovieTag> MovieTags { get; } = [];
        public List<Tag> Tags { get; } = [];
    }
}
