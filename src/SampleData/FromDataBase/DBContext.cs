using Microsoft.EntityFrameworkCore;
using src.SampleData.Common;

namespace src.SampleData.FromDataBase
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<PersonFromSource> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonFromSource>()
                .Property(p => p.Name) 
                .HasColumnName("FirstName");  
        }
    }
}