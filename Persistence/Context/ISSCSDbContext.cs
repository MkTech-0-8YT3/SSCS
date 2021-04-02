using Common.DataModels.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public interface ISSCSDbContext
    {
        DatabaseFacade Database { get; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        Task<int> SaveChangesAsync();
    }
}
