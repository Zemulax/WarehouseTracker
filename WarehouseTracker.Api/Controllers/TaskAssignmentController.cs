using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application.Services;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAssignmentController : ControllerBase
    {

        // Implementation for TaskAssignmentController goes here.

        private readonly ITaskAssignmentService _taskAssignmentService;

        public TaskAssignmentController(ITaskAssignmentService taskAssignmentService)
        {
            _taskAssignmentService = taskAssignmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskAssignmentDto taskAssignmentDto)
        {
            var shiftAssignmentDomain = new TaskAssignment
            {
                ColleagueId = taskAssignmentDto.ColleagueId,
                
                ShiftStart = taskAssignmentDto.ShiftStart.ToUniversalTime(),
                ShiftEnd = taskAssignmentDto.ShiftEnd.ToUniversalTime(),

            };

            await _taskAssignmentService.CreateAsync(shiftAssignmentDomain);
            return Created("shift was created", null);
        }

        
        
    }
}
