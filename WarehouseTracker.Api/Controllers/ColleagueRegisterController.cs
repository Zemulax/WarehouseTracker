using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
    public class ColleagueRegisterController : ControllerBase
    {
        private readonly IColleagueService _colleagueService;

        public ColleagueRegisterController(IColleagueService colleagueService)
        {
            _colleagueService = colleagueService;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterColleague([FromBody] Colleague request)
        {
            // Implementation for registering a colleague goes here.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _colleagueService.RegisterColleagueAsync(
                request.EmployeeId, 
                request.FirstName, 
                request.LastName
                );

            return Ok("Successfullly saved colleague");



        }
    }
}
