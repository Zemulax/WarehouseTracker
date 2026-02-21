using WarehouseTracker.Api.Enums;
using WarehouseTracker.Domain.Enums;

namespace WarehouseTracker.Api.Models
{
    public class BreakRuleDto
    {
        public BreakTypes BreakType { get; set; }
        public TimeOnly BreakStart { get; set; }
        public TimeOnly BreakEnd { get; set; }
    }
}
