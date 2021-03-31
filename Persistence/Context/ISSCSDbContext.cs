using Common.DataModels.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public interface ISSCSDbContext
    {
        DatabaseFacade Database { get; }
        public DbSet<Student> Accounts { get; set; }
        Task<int> SaveChangesAsync();
    }
}
