using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application.ActivitySessions;

namespace WarehouseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitySessionController : ControllerBase
    {
        private readonly IActivitySessionService _activitySessionService;
        public ActivitySessionController(IActivitySessionService activitySessionService)
        {
            _activitySessionService = activitySessionService;
        }

        [HttpGet]
        public async Task<IActionResult>GetByShift(int? shiftId, string? colleagueId)
        {
            if (shiftId == null && colleagueId == null)
                return BadRequest("At least one filter must be provided.");
            var sessions = await _activitySessionService.GetByShiftAsync(shiftId, colleagueId);
            return Ok(sessions);
        }

    }
}
