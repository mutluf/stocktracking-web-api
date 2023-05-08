using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTracking.Domain.Entities
{
    public class StockMovementType : BaseEntity
    {
        public string Type { get; set; }
    }
}
