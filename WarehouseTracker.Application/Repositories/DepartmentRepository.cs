using Microsoft.EntityFrameworkCore;
using WarehouseTracker.Application.Services;
using WarehouseTracker.Domain;
using WarehouseTracker.Infrastructure;

namespace WarehouseTracker.Application.Repositories
{
    public class DepartmentRepository : IDepartmentService
    {

        private readonly WarehouseTrackerDbContext _dbContext;

        public DepartmentRepository(WarehouseTrackerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task RegisterDepartmentAsync(
            int Id,
            string departmentName,
            string deparmentCode,
            string departmentGroupCode)
        {
            var department = new Department
            {
                Id = Id,
                DepartmentName = departmentName,
                DeparmentCode = deparmentCode,
                DepartmentGroupCode = departmentGroupCode
            };
            _dbContext.Departments.Add(department);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Department>> RetrieveDepartmentAsync()
        {
            return await _dbContext.Departments.ToListAsync();
        }

        public async Task<List<Department>> RetrieveDepartmentByAttribute(
            int? Id,
            string? departmentName,
            string? deparmentCode,
            string? departmentGroupCode
            )
        {
            var query = _dbContext.Departments.AsQueryable();
            if (Id.HasValue)
            {
                query = query.Where(d => d.Id == Id.Value);
            }
            if (!string.IsNullOrEmpty(departmentName))
            {
                query = query.Where(d => d.DepartmentName == departmentName);
            }
            if (!string.IsNullOrEmpty(deparmentCode))
            {
                query = query.Where(d => d.DeparmentCode == deparmentCode);
            }
            if (!string.IsNullOrEmpty(departmentGroupCode))
            {
                query = query.Where(d => d.DepartmentGroupCode == departmentGroupCode);
            }
            return await query.ToListAsync();
        }

        public async Task DeleteDepartmentAsync(string deparmentCode)
        {
            var department = await _dbContext.Departments
                .FirstOrDefaultAsync(d => d.DeparmentCode == deparmentCode);
            if (department != null)
            {
                _dbContext.Departments.Remove(department);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateDepartmentAsync(
            string deparmentCode,
            string? departmentName,
            string? departmentGroupCode)
        {
            var department = await _dbContext.Departments
                .FirstOrDefaultAsync(d => d.DeparmentCode == deparmentCode);
            if (department != null)
            {
                if (!string.IsNullOrEmpty(departmentName))
                {
                    department.DepartmentName = departmentName;
                }
                if (!string.IsNullOrEmpty(departmentGroupCode))
                {
                    department.DepartmentGroupCode = departmentGroupCode;
                }
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
