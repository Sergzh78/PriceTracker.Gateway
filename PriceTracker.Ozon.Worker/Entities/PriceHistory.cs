using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceTracker.Ozon.Worker.Entities
{
    public class PriceHistory
    {
        public Guid Id { get; set; }
        public Guid OzonTaskId { get; set; }
        public decimal Price { get; set; }
        public DateTime CheckedAt { get; set; }

        public OzonTask? OzonTask { get; set; }
    }
}
