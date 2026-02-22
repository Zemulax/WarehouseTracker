using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseTracker.Domain;
using WarehouseTracker.Infrastructure;

namespace WarehouseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly WarehouseTrackerDbContext _db;

        public DashboardController(WarehouseTrackerDbContext db) { 
         _db = db;
        }

        [HttpGet("live")]
        public async Task<IActionResult> GetLiveWorkUpdates()
        {
            var result = await _db.ActivitySessions
            .Where(s => s.SessionStart ==
                _db.ActivitySessions
                    .Where(x => x.ColleagueId == s.ColleagueId)
                    .Max(x => x.SessionStart))
            .Select(s => new
            {
                s.ColleagueId,
                s.WorkDayId,
                s.SessionType,
                s.SessionStart,
                s.SessionEnd
            })
            .ToListAsync();

            return Ok(result);
        }
    }
}
