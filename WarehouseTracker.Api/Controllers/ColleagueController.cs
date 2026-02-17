using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application.Services;
using WarehouseTracker.Domain;

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
        public async Task< IActionResult> CreateColleague(ColleagueDTO request)
        {
            var colleague = new Colleague
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

        [HttpGet("{colleagueId}")]
        public async Task<ColleagueDTO> GetColleagueAsync(string colleagueId )
        {
            var colleagueDomain =  await _colleagueService.GetColleagueById(colleagueId);

            return new ColleagueDTO
            {

                ColleagueId = colleagueDomain.ColleagueId,
                FirstName = colleagueDomain.FirstName,
                LastName = colleagueDomain.LastName,
                Role = colleagueDomain.Role,
                IsActive = colleagueDomain.IsActive,

            };
        }

        [HttpGet]
        public async Task<List<ColleagueDTO>> GetColleaguesAsync()
        {
            var colleagues = await _colleagueService.RetrieveColleagueAsync();

            return colleagues.Select(c => new ColleagueDTO{
                ColleagueId=c.ColleagueId,
                FirstName=c.FirstName,
                LastName=c.LastName,
                Role=c.Role,
                IsActive=c.IsActive,
            }).ToList();
        }
    }

}
