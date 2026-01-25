using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application;

namespace WarehouseTracker.Api.Controllers
{
    /// <summary>
    /// API endpoint for registering a new colleague
    /// Accepts a JSON payload with colleague details via [FromBody]
    /// Maps the request to a RegisterColleagueRequest model
    /// calls a service to handle the registration logic
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ColleagueController : ControllerBase
    {
        private readonly IColleagueService _colleagueService;

        public ColleagueController(IColleagueService colleagueService)
        {
            _colleagueService = colleagueService;
        }

        //register a colleague
        [HttpPost]
        public async Task<IActionResult> RegisterColleague([FromBody] Colleague request)
        {
            // Implementation for registering a colleague goes here.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _colleagueService.RegisterColleagueAsync(
                request.Id,
                request.EmployeeId,
                request.FirstName,
                request.LastName,
                request.Role,
                request.IsActive
                );

            return Ok("Successfullly saved colleague");
        }

        //retrieve all colleagues
        [HttpGet]
        public async Task<IActionResult> RetrieveAllColleagues()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var colleagues = await _colleagueService.RetrieveColleagueAsync();
            return Ok(colleagues);
        }

        //retrieve colleague by attribute(id,name...etc)
        [HttpGet("filter-by")]
        public async Task<IActionResult> RetrieveColleagueByAttributeAsync(
            int? id = null,
            string? employeeId = null,
            string? firstName = null,
            string? lastName = null,
            string? role = null,
            bool? isActive = null
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var colleague = await _colleagueService.RetrieveColleagueByAttribute(
                id, employeeId, firstName, lastName, role, isActive
                );

            return Ok(colleague);
        }

        //delete colleague
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteColleague(string employeeId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _colleagueService.DeleteColleagueAsync(employeeId);
            return Ok("Successfully deleted colleague");
        }

        //update colleague
        [HttpPut ("{employeeId}")]
        public async Task<IActionResult> UpdateColleague(
            string employeeId,
            string? firstName = null,
            string? lastName = null,
            string? role = null,
            bool? isActive = null
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _colleagueService.UpdateColleagueAsync(
              employeeId, firstName, lastName, role, isActive
                );

            return Ok("Successfully updated colleague");
        }

    }

}
