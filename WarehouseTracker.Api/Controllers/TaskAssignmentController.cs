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
        private readonly IWorkDayService _workDayService;

        public TaskAssignmentController(ITaskAssignmentService taskAssignmentService, IWorkDayService workDayService)
        {
            _workDayService = workDayService;
            _taskAssignmentService = taskAssignmentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskAssignmentDto taskAssignmentDto)
        {
            var existingWorkDay = await _workDayService.GetActiveWorkDay(taskAssignmentDto.ColleagueId);
            if (existingWorkDay == null)
            {
                throw new InvalidOperationException($"Colleague {taskAssignmentDto.ColleagueId} does not have an active work day.");
            }
            try
            {
                var taskAssignmentDomain = new TaskAssignment
                {
                    ColleagueId = taskAssignmentDto.ColleagueId,
                    WorkDayId = existingWorkDay.Id,
                    DepartmentCode = taskAssignmentDto.DepartmentCode,

                };

                await _taskAssignmentService.CreateAsync(taskAssignmentDomain);
                return Created("shift was created", null);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }



        }
    }
}
