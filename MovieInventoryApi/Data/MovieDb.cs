using Microsoft.EntityFrameworkCore;
using MovieInventoryAPI.Models;

namespace MovieInventoryApi.Data
{
    public class MovieDb : DbContext
    {
        public MovieDb(DbContextOptions<MovieDb> options)
        : base(options) { }

        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Studio> Studios => Set<Studio>();
        public DbSet<Director> Directors => Set<Director>();
        public DbSet<Actor> Actors => Set<Actor>();
        public DbSet<Actress> Actresses => Set<Actress>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<MovieTag> MovieTags => Set<MovieTag>();
        public DbSet<MovieDirector> MovieDirectors => Set<MovieDirector>();
        public DbSet<MovieActor> MovieActors => Set<MovieActor>();
        public DbSet<MovieActress> MovieActresses => Set<MovieActress>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieTag>()
                .HasKey(mp => new { mp.MovieId, mp.TagId });
            modelBuilder.Entity<MovieDirector>()
                .HasKey(md => new { md.MovieId, md.DirectorId });
            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });
            modelBuilder.Entity<MovieActress>()
                .HasKey(ma => new { ma.MovieId, ma.ActressId });
            modelBuilder.Entity<Tag>()
                    .HasOne(t => t.ParentTag)
                    .WithMany(t => t.ChildTags)
                    .HasForeignKey(t => t.ParentTagId)
                    .OnDelete(DeleteBehavior.Restrict); // Prevents accidental deletion of child tags
        }
    }
}
