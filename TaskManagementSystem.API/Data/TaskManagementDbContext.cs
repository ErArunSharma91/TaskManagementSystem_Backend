using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.API.Models;

namespace TaskManagementSystem.API.Data
{
    public class TaskManagementDbContext : DbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserTask> Tasks { get; set; }
        public DbSet<User> Users { get; set; }  // Add this line

        // Override OnConfiguring to add retry logic if it is not configured in Startup.cs
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    "Server=localhost;Database=YourDatabaseName;User=root;Password=redhat@123;Port=3306;",
                    new MySqlServerVersion(new Version(8, 0, 21)), // Use the version of MySQL you're using
                    options => options.EnableRetryOnFailure() // Enabling retry on transient failures
                );
            }
        }
    }
}
