
using Microsoft.AspNetCore.Mvc;
using WarehouseTracker.Api.Enums;
using WarehouseTracker.Api.Models;
using WarehouseTracker.Application.Services;
using WarehouseTracker.Domain;
using WarehouseTracker.Domain.Enums;

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
        public async Task<IActionResult> Create(BreakRuleDTO breakRuleDTO)
        {
            var existing = await _breakRuleService.GetBreakByType(breakRuleDTO.BreakType);
            if (existing == null)
            {
                var breakDomain = new Domain.BreakRule
                {
                    BreakType = breakRuleDTO.BreakType,
                    BreakStart = breakRuleDTO.BreakStart,
                    BreakEnd = breakRuleDTO.BreakEnd,
                };
                await _breakRuleService.AddAsync(breakDomain);
                return (Ok("Break was added"));
            }
            else { 
            return BadRequest("Break Already Exists");
            }

           
        }

        [HttpGet]
        public async Task<BreakRuleDTO?> GetBreakByType(BreakTypes breakType)
        {
          var existing = await _breakRuleService.GetBreakByType(breakType);
            if (existing == null) { 
              return null;
            }
            var getBreak = new BreakRuleDTO
            {
                BreakType = existing.BreakType,
                BreakStart = existing.BreakStart,
                BreakEnd = existing.BreakEnd,
            };
            return(getBreak);

        }
        [HttpPut]
        public async Task<IActionResult>UpdateBreak(BreakRuleDTO breakRuleDTO)
        {
            var breakDomain = new BreakRule
            {
                BreakEnd = breakRuleDTO.BreakEnd,
                BreakType = breakRuleDTO.BreakType,
                BreakStart = breakRuleDTO.BreakStart,
            };
           await  _breakRuleService.UpdateAsync(breakDomain);
            return (Ok("Break Updated!"));
        }

    }
}
