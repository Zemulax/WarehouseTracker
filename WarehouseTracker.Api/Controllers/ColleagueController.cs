using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application.Services;

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

        [HttpPost]
        public async Task< IActionResult> CreateColleague(Colleague request)
        {
            var colleague = new Domain.Colleague
            {
                ColleagueId = request.ColleagueId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = request.Role,
                IsActive = request.IsActive,
            };

            await _colleagueService.RegisterColleagueAsync(colleague);
            return Created("Colleague Added Successfully", null);

        }
    }

}
