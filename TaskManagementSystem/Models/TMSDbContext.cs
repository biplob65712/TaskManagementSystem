using Microsoft.EntityFrameworkCore;

namespace TaskManagementSystem.Models
{
    public class TMSDbContext : DbContext
    {
        public DbSet<User> UserTB { get; set; }
        public DbSet<TaskModel> TaskTB { get; set; }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string connectionString = @"Server=DESKTOP-CR30BFH\SQLEXPRESS; Database=TMS_DB; Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connectionString);


        }
    }
}
