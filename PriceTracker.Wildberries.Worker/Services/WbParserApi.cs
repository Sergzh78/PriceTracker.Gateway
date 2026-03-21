using PriceTracker.Shared.Infrastructure.Http;
using PriceTracker.Wildberries.Worker.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceTracker.Wildberries.Worker.Services
{
    public class WbParserApi : IWbParserApi
    {
        private readonly IHttpApiClient _httpApiClient;

        private readonly string _wbApiParsePriceEndpoint = "http://opars3245.cmpl.ru/api/v1/wbprice";

        public WbParserApi(IHttpApiClient httpApiClient)
        {
            _httpApiClient = httpApiClient;
        }

        public async Task<WbParserResponse?> GetPriceAsync(string url, CancellationToken cancellationToken = default)
        {
            var requesData = new
            {
                productUrl = url,
                ignoreDiscount = true,
                persistence = 12
            };

            var token = Environment.GetEnvironmentVariable("WB_PARSER_API_TOKEN");
            _httpApiClient.SetBearerToken(token);

            var response = await _httpApiClient.PostAsync<WbParserResponse>(_wbApiParsePriceEndpoint, requesData, cancellationToken);
            if (response == null)
            {
                //logger
            }

            return response;
        }
    }
}
