using Microsoft.EntityFrameworkCore;
using ConwayGame.Models;

namespace ConwayGame
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Board> Boards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Board>()
                .Property(b => b.CurrentState)
                .IsRequired();

            modelBuilder.Entity<Board>()
                .Property(b => b.InitialState)
                .IsRequired();

            modelBuilder.Entity<Board>()
                .Property(b => b.CurrentStep)
                .IsRequired()
                .HasDefaultValue(0);
        }
    }
}
