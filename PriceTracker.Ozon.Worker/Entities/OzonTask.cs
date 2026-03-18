using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceTracker.Ozon.Worker.Entities
{
    public class OzonTask
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
