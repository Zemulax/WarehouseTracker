using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Enums;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        // Implementation for EventsController goes here.

        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public async Task<IActionResult> LogEvent([FromBody] Domain.Event request)
        {
            // Implementation for creating an event goes here.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _eventService.LogEventAsync(
                request.EventType,
                request.ColleagueId,
                request.DepartmentId,
                request.Timestamp,
                request.Source
                );
            return Ok($"Event {request.EventType} created successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> RetrieveAllEvents()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var events = await _eventService.RetrieveEventsAsync();
            return Ok(events);
        }

        [HttpGet("search")]
        public async Task<IActionResult> RetrieveEventsByAttribute(
            [FromQuery] Guid? id = null,
            [FromQuery] string? eventType = null,
            [FromQuery] int? colleagueId = null,
            [FromQuery] int? departmentId = null,
            [FromQuery] DateTime? timestamp = null,
            [FromQuery] string? source = null
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var events = await _eventService.RetrieveEventsByAttribute(
                id,
                colleagueId,
                timestamp,
                eventType,
                departmentId,
                source
                );
            return Ok(events);
        }
    }
}
