using LikeTrackerAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace LikeTrackerAPI.Data
{
    public class LikeTrackerContext : DbContext
    {
        public LikeTrackerContext(DbContextOptions<LikeTrackerContext> options) : base(options) 
        {

        }

        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Optional: Seed initial data or configure relationships if needed
            modelBuilder.Entity<Article>()
                .HasData(new Article { Id = 1, Title = "Sample Article", LikesCount = 0 });
        }
    }
}
