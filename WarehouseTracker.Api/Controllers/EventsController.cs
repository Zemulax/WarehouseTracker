using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application.Services;
using WarehouseTracker.Domain;

namespace WarehouseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController (IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(EventDTO request)
        {
            var eventDomain = new Event
            {
                ColleagueId = request.ColleagueId,
                DepartmentCode = request.DepartmentCode,
                EventType = request.EventType,
                TimestampUtc = request.TimestampUtc,

            };
            await _eventService.CreateEventAsync(eventDomain);
            return Ok();
        }
    }
}
