using Microsoft.EntityFrameworkCore;
using WarehouseTracker.Application.Services;
using WarehouseTracker.Domain;
using WarehouseTracker.Infrastructure;

namespace WarehouseTracker.Application.Repositories
{
    public class DepartmentRepository : IDepartmenRepository
    {
        private readonly WarehouseTrackerDbContext _dbContext;

        public DepartmentRepository(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Department department)
        {
            await _dbContext.Departments.AddAsync(department);
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _dbContext.Departments.ToListAsync();
        }

        public async Task<Department?> GetByCodeAsync(string departmentCode)
        {
            return await _dbContext.Departments
                .FirstOrDefaultAsync(d => d.DeparmentCode == departmentCode);
        }

        public async Task SaveChangeAsync()
        {
           await _dbContext.SaveChangesAsync();
        }
    }
}
