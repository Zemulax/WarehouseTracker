using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseTracker.Domain
{
    public class BreakRule
    {
        public int Id { get; set; }
        public string BreakType { get; set; } = null!;
        public TimeOnly BreakStart { get; set; }
        public TimeOnly BreakEnd { get; set; }
    }
}
