using Microsoft.EntityFrameworkCore;

using TasksApp.Data.Models;


namespace TasksApp.Data
{
    public class TaskAppDbContext : DbContext
    {
        public TaskAppDbContext(DbContextOptions<TaskAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<Status> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Task>()
                .HasOne<Status>(t => t.Status);
        }
    }
}