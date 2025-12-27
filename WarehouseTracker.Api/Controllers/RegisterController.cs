using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WarehouseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        [HttpPost]
        public Task<IActionResult> RegisterColleague([FromBody] Domain.Colleague request)
        {
            if (!ModelState.IsValid)
            {
                return Task.FromResult<IActionResult>(BadRequest(ModelState));
            }

            //Todo: Add logic to save the colleague to the database

            //for now a fake db
            var createdId = 1; // Simulate created colleague ID

            return Task.FromResult<IActionResult>(CreatedAtAction(nameof(GetColleagueById),
                new { id = createdId },
                new { Id = createdId, request.FirstName, request.LastName }));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetColleagueById(int id)
        {
            //Todo: Add logic to retrieve the colleague from the database
            return Ok(new { id });
        }
    }
}
