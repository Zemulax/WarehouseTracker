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
        private readonly IDepartmenRepository _departmenRepository;

        public DepartmentService(IDepartmenRepository departmenRepository)
        {
            _departmenRepository = departmenRepository;
        }

        public async Task CreateAsync(Department request)
        {
            var existing = await _departmenRepository.GetByCodeAsync(request.DeparmentCode);
            if (existing != null)
            {
                throw new Exception("Department already esists");
            }

            var NewDept = new Department
            {
                DepartmentName = request.DepartmentName,
                DeparmentCode = request.DeparmentCode,
                DepartmentGroupCode = request.DepartmentGroupCode

            };

            await _departmenRepository.AddAsync(NewDept);
            await _departmenRepository.SaveChangeAsync();
        }

        public Task<List<Department>> GetAllAsync()
        {
            return _departmenRepository.GetAllAsync();
        }

        public Task<Department?> GetByCodeAsync(string code)
        {
            
            var dept = _departmenRepository.GetByCodeAsync(code);

            if (dept != null)
            {
                return dept;
            }

            throw new ArgumentNullException("department code does not exist");

        }
    }
}
