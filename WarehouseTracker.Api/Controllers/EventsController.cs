using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CreateEvent(Event request)
        {
            await _eventService.CreateEventAsync(request);
            return Ok();
        }
    }
}
