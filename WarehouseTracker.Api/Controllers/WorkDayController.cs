using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application.Services;

namespace WarehouseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkDayController : ControllerBase
    {
        private readonly IWorkDayService _workDayService;

        public WorkDayController(IWorkDayService workDayService)
        {
            _workDayService = workDayService;
        }

        [HttpGet("active/{colleagueId}")]
        public async Task<IActionResult> GetActiveWorkDay(string colleagueId)
        {
            var colleagueActiveDay = await _workDayService.GetActiveWorkDay(colleagueId);
            if (colleagueActiveDay == null)
            {
                return NotFound("No Active workday found for colleague");
            }

            var workdayDto = new WorkDayDto
            {
                ColleagueId = colleagueActiveDay.ColleagueId,
                Colleague = colleagueActiveDay.Colleague,
                WorkDayStart = colleagueActiveDay.WorkDayStart,
                Status = colleagueActiveDay.Status,
                WorkDayEnd = colleagueActiveDay.WorkDayEnd,
                TaskAssignments = colleagueActiveDay.TaskAssignments
            };
            return Ok(workdayDto);

        }

        [HttpPost("Signin{colleagueId}")]
        public async Task<IActionResult> StartWorkDay(string colleagueId)
        {
            try
            {
                await _workDayService.StartWorkDayAsync(colleagueId);
                return Ok("signed in successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sign-out{colleagueId}")]
        public async Task<IActionResult> EndWorkDay(string colleagueId)
        {
            try
            {
                await _workDayService.EndWorkDayAsync(colleagueId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
