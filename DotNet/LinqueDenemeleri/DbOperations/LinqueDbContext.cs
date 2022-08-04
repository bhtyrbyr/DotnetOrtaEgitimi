using Microsoft.EntityFrameworkCore;

namespace LinqueDenemeleri.DbOperations
{
    public class LinqueDbContext : DbContext
    {
        public DbSet<Student> Students{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName:"linqueDatabase");
        }
    }
}