using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TESTINGAPP.Models
{
    public class RecordContext : DbContext
    {
        public RecordContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=RecordDB;Trusted_Connection=True;");
        }

        public RecordContext(DbContextOptions<RecordContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Record> Records { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
