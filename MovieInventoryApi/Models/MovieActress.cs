namespace MovieInventoryAPI.Models
{
    public class MovieActress
    {
        public int MovieId { get; set; }
        public int ActressId { get; set; }
        public Movie Movie { get; set; } = null!;
        public Actress Actress { get; set; } = null!;
    }
}
