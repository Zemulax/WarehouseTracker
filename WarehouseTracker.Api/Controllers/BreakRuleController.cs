
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Application;

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

        [HttpPost]
        public async Task<IActionResult> CreateBreakRule([FromBody] Models.BreakRule request)
        {
            // Implementation for creating a break rule goes here.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var createdBreak = await _breakRuleService.SetBreakRule(
                request.BreakType,
                request.StartAfterMinutes,
                request.DurationMinutes
                );
            if(!createdBreak)
            {
                return Conflict("Break Rule Already Exists, but it can be updated");
            }
            return Ok("Break Rule Created Successfully");


        }

        [HttpGet]
        public async Task<IActionResult> RetrieveAllBreakRules()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var breakRules = await _breakRuleService.GetBreakRulesAsync();
            return Ok(breakRules);
        }

        //start from here. need to update databse and rerun
        [HttpPut("breakType")]
        public async Task<IActionResult> UpdateBreakRule([FromBody] Models.BreakRule request)
        {
            // Implementation for updating a break rule goes here.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

             await _breakRuleService.SetBreakRule(
                
                request.BreakType,
                request.StartAfterMinutes,
                request.DurationMinutes
                );
            
            return Ok($"Break Rule Updated Successfully");
        }

        [HttpGet("search")]
        public async Task<IActionResult> RetrieveBreakRuleByType(
            [FromQuery] string breakType
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var breakRule = await _breakRuleService.GetBreakRuleByTypeAsync(breakType);
            return Ok(breakRule);
        }


    }
}
