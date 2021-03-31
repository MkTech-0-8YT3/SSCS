using Common.DataModels.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    class SSCSDbContext : DbContext, ISSCSDbContext
    {
        public SSCSDbContext(DbContextOptions<SSCSDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
