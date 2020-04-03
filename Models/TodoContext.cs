using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<Batteries>()
        //         .HasOne(p => p.buildings)
        //         .WithMany(b => b.batteries);
        // }

        public DbSet<Buildings> buildings { get; set; }
        public DbSet<Battery> batteries { get; set; }
        public DbSet<Columns> columns { get; set; }
        public DbSet<Elevators> elevators { get; set; }
        public DbSet<Leads> leads { get; set; }
        public DbSet<Customers> customers { get; set; }
    }
}