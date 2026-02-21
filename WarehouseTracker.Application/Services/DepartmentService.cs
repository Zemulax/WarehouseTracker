using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Application.Repositories;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task CreateAsync(Department request)
        {
            var existing = await _departmentRepository.GetByCodeAsync(request.DepartmentCode);
            if (existing != null)
            {
                throw new Exception("Department already esists");
            }

            var newDept = new Department
            {
                DepartmentName = request.DepartmentName,
                DepartmentCode = request.DepartmentCode,
                DepartmentGroupCode = request.DepartmentGroupCode

            };

            await _departmentRepository.AddAsync(newDept);
            await _departmentRepository.SaveChangeAsync();
        }

        public Task<List<Department>> GetAllAsync()
        {
            return _departmentRepository.GetAllAsync();
        }

        public Task<Department?> GetByCodeAsync(string code)
        {
            
            var dept = _departmentRepository.GetByCodeAsync(code);

            return dept;

        }
    }
}
