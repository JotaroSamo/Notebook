using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TESTINGAPP.Models.DB
{
    public class RecordContext : DbContext
    {
        public RecordContext(DbContextOptions<RecordContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Record> Records { get; set; }
       
    }
}
