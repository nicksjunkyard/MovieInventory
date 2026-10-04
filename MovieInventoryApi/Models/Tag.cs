using System.ComponentModel.DataAnnotations;

namespace MovieInventoryAPI.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public List<string> Synonyms { get; set; } = [];
        public int? ParentTagId { get; set; } // Nullable foreign key for the parent

        // Navigation property for the parent tag
        public virtual Tag ParentTag { get; set; }

        // Navigation property for child tags
        public virtual ICollection<Tag> ChildTags { get; set; } = new HashSet<Tag>();
        public List<MovieTag> MoviePactions { get; } = [];
    }
}