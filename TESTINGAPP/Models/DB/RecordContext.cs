using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TESTINGAPP.Models.DB
{
    public class RecordContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=RecordDB;Trusted_Connection=True;");
        }

        public DbSet<Record> Records { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
