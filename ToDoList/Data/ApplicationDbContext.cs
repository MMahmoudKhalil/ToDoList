using Microsoft.EntityFrameworkCore;
namespace ToDoList.Data.Model
    
{
    public class ApplicationDbContext : DbContext 
    {
        public DbSet<ToDo> todos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ToDoList;Integrated Security=True;TrustServerCertificate=True");
        }
    }
}
