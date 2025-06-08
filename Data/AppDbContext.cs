using Microsoft.EntityFrameworkCore;
using PresentationApp.Models;

namespace PresentationApp.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<PresentationUser> PresentationUsers { get; set; }
        public DbSet<DrawingPath> DrawingPaths { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<TextBlock> TextBlocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Presentation>()
                .HasMany(p => p.Slides)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
