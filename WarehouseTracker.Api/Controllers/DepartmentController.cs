using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application.Services;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        // Implementation for DepartmentController goes here.
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentDTO department)
        {
            var domainDepartment = new Department   
            {
                DepartmentName = department.DepartmentName,
                DeparmentCode = department.DeparmentCode,
                DepartmentGroupCode = department.DepartmentGroupCode,
            };

            await _departmentService.CreateAsync(domainDepartment);
            return Created("Department Was Added", null);
        }

        [HttpGet]
        public async Task<List<DepartmentDTO>> GetAll() { 
        
         var domainDepartments = await _departmentService.GetAllAsync();

            return domainDepartments.Select(d => new DepartmentDTO
            { 
                DepartmentName = d.DepartmentName,
                DeparmentCode = d.DeparmentCode,
                DepartmentGroupCode= d.DepartmentGroupCode,
            }).ToList();
        }

        [HttpGet("{departmentCode}")]
        public async Task<DepartmentDTO?> GetDepartmentAsync(string departmentCode)
        {
            var domainDepartment = await _departmentService.GetByCodeAsync(departmentCode);

            if (domainDepartment != null)
            {
                return new DepartmentDTO
                {
                    DepartmentName = domainDepartment.DepartmentName,
                    DeparmentCode = domainDepartment.DeparmentCode,
                    DepartmentGroupCode = domainDepartment.DepartmentGroupCode
                };
            }

            return null;
        }

    }
}
