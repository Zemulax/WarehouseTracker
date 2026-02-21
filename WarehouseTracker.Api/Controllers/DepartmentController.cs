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
        public async Task<IActionResult> CreateDepartment(DepartmentDto department)
        {
            var domainDepartment = new Department   
            {
                DepartmentName = department.DepartmentName,
                DepartmentCode = department.DepartmentCode,
                DepartmentGroupCode = department.DepartmentGroupCode,
            };

            await _departmentService.CreateAsync(domainDepartment);
            return Created("Department Was Added", null);
        }

        [HttpGet]
        public async Task<List<DepartmentDto>> GetAll() { 
        
         var domainDepartments = await _departmentService.GetAllAsync();

            return domainDepartments.Select(d => new DepartmentDto
            { 
                DepartmentName = d.DepartmentName,
                DepartmentCode = d.DepartmentCode,
                DepartmentGroupCode= d.DepartmentGroupCode,
            }).ToList();
        }

        [HttpGet("{departmentCode}")]
        public async Task<DepartmentDto?> GetDepartmentAsync(string departmentCode)
        {
            var domainDepartment = await _departmentService.GetByCodeAsync(departmentCode);

            if (domainDepartment != null)
            {
                return new DepartmentDto
                {
                    DepartmentName = domainDepartment.DepartmentName,
                    DepartmentCode = domainDepartment.DepartmentCode,
                    DepartmentGroupCode = domainDepartment.DepartmentGroupCode
                };
            }

            return null;
        }

    }
}
