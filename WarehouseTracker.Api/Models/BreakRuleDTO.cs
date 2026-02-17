namespace WarehouseTracker.Api.Models
{
    public class BreakRuleDTO
    {
        public string BreakType { get; set; } = null!;
        public int StartAfterMinutes { get; set; }
        public int DurationMinutes { get; set; }
    }
}
