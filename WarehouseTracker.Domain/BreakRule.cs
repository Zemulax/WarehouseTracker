using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseTracker.Domain.Enums;

namespace WarehouseTracker.Domain
{
    public class BreakRule
    {
        public int Id { get; set; }
        public BreakTypes BreakType { get; set; }
        public TimeOnly BreakStart { get; set; }
        public TimeOnly BreakEnd { get; set; }
    }
}
