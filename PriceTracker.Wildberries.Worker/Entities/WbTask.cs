using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceTracker.Wildberries.Worker.Entities
{
    public class WbTask
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public decimal ThresholdPrice { get; set; }
        public DateTime ParseFromDate { get; set; }
        public DateTime ParseToDate { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<PriceHistory> PriceHistories { get; set; }
    }
}
