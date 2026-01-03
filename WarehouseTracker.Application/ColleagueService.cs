using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain;
using WarehouseTracker.Infrastructure;

namespace WarehouseTracker.Application
{

    public class ColleagueService : IColleagueService
    {
        private readonly WarehouseTrackerDbContext _dbContext;

        public ColleagueService(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task RegisterColleagueAsync(string employeeId, string firstName, string lastName)
        {
          var colleague = new Colleague
            {
                ColleagueId = employeeId,
                FirstName = firstName,
                LastName = lastName
            };
            _dbContext.Colleagues.Add(colleague);
            await  _dbContext.SaveChangesAsync();
        }
            
    }
}
