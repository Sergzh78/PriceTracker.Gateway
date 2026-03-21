using PriceTracker.Wildberries.Worker.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceTracker.Wildberries.Worker.Services
{
    public interface IWbParserApi
    {
        Task<WbParserResponse?> GetPriceAsync(string url, CancellationToken cancellationToken = default);
    }
}
