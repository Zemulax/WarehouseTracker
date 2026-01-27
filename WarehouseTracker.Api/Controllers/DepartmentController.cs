using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application.Services;

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
        public async Task<IActionResult> RegisterDepartment([FromBody] Department request)
        {
            // Implementation for registering a department goes here.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _departmentService.RegisterDepartmentAsync(
                request.Id,
                request.DepartmentName,
                request.DeparmentCode,
                request.DepartmentGroupCode
                );

            return Ok("Department was Created");
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveAllDepartments()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var departments = await _departmentService.RetrieveDepartmentAsync();
            return Ok(departments);
        }

        [HttpGet("search")]
        public async Task<IActionResult> RetrieveDepartmentByAttribute(
            [FromQuery] int? id = null,
            [FromQuery] string? departmentName = null,
            [FromQuery] string? deparmentCode = null,
            [FromQuery] string? departmentGroupCode = null
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var departments = await _departmentService.RetrieveDepartmentByAttribute(
                id,
                departmentName,
                deparmentCode,
                departmentGroupCode
                );
            return Ok(departments);
        }
        [HttpDelete("{deparmentCode}")]
        public async Task<IActionResult> DeleteDepartment([FromRoute] string deparmentCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _departmentService.DeleteDepartmentAsync(deparmentCode);
            return Ok("Department was deleted successfully");
        }

        [HttpPut("{deparmentCode}")]
        public async Task<IActionResult> UpdateDepartment(
            [FromRoute] string deparmentCode,
            [FromBody] Department request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _departmentService.UpdateDepartmentAsync(
                deparmentCode,
                request.DepartmentName,
                request.DepartmentGroupCode
                );
            return Ok("Department was updated successfully");
        }


    }
}
