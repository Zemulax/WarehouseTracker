using Microsoft.EntityFrameworkCore;
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
        public async Task RegisterColleagueAsync(
            int Id,
            string employeeId, 
            string firstName, 
            string lastName,
            string role,
            bool isActive)
        {
          var colleague = new Colleague
            {  
              Id = Id,
              EmployeeId = employeeId,
              FirstName = firstName,
                LastName = lastName,
                Role = role,
                IsActive = isActive
          };
            _dbContext.Colleagues.Add(colleague);
            await  _dbContext.SaveChangesAsync();
        }

        public async Task<List<Colleague>> RetrieveColleagueAsync()
        {
            // Implementation for retrieving a colleague goes here.
            return await _dbContext.Colleagues.ToListAsync();
        }

        public async Task<List<Colleague>> RetrieveColleagueByAttribute(
            int? id = null,
            string? employeeId = null,
            string? firstName = null,
            string? lastName = null,
            string? role = null,
            bool? isActive = null
            )
        {
            // Implementation for retrieving a colleague by ID goes here.
            var query = _dbContext.Colleagues.AsQueryable();
            if (id.HasValue)
            {
                query = query.Where(c => c.Id == id.Value);
            }
            if (!string.IsNullOrEmpty(employeeId))
            {
                query = query.Where(c => c.EmployeeId == employeeId);
            }
            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(c => c.FirstName == firstName);
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(c => c.LastName == lastName);
            }
            if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(c => c.Role == role);
            }
            if (isActive.HasValue)
            {
                query = query.Where(c => c.IsActive == isActive.Value);
            }
            return await query.ToListAsync();
        }

        public async Task DeleteColleagueAsync(string employeeId)
        {
            var colleague = await _dbContext.Colleagues
                .FirstOrDefaultAsync(c => c.EmployeeId == employeeId);
            if (colleague != null)
            {
                _dbContext.Colleagues.Remove(colleague);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateColleagueAsync(
            string? employeeId,
            string? firstName,
            string? lastName,
            string? role,
            bool? isActive)
        {
            var colleague = await _dbContext.Colleagues
                .FirstOrDefaultAsync(c => c.EmployeeId == employeeId);
            if (colleague != null)
            {
                if (!string.IsNullOrEmpty(firstName))
                {
                    colleague.FirstName = firstName;
                }
                if (!string.IsNullOrEmpty(lastName))
                {
                    colleague.LastName = lastName;
                }
                if (!string.IsNullOrEmpty(role))
                {
                    colleague.Role = role;
                }
                if (isActive.HasValue)
                {
                    colleague.IsActive = isActive.Value;
                }
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
