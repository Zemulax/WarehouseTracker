
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Application.Services;

namespace WarehouseTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreakRuleController : ControllerBase
    {

        // Implementation for BreakRuleController goes here.

        private readonly IBreakRuleService _breakRuleService;

        public BreakRuleController(IBreakRuleService breakRuleService)
        {
            _breakRuleService = breakRuleService;
        }

    }
}
