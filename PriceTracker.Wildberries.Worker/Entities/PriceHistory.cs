using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceTracker.Wildberries.Worker.Entities
{
    public class PriceHistory
    {
        public Guid Id { get; set; }
        public Guid WbTaskId { get; set; }
        public decimal Price { get; set; }
        public DateTime CheckedAt { get; set; }

        public WbTask? WbTask { get; set; }
    }
}
