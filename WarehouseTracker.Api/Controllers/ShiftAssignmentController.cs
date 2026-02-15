using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application.Services;

namespace WarehouseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftAssignmentController : ControllerBase
    {

        // Implementation for ShiftAssignmentController goes here.

        private readonly IShiftAssignmentService _shiftAssignmentService;

        public ShiftAssignmentController(IShiftAssignmentService shiftAssignmentService)
        {
            _shiftAssignmentService = shiftAssignmentService;
        }
    }
}
