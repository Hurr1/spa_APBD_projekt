using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using APDBprojekt.Shared.Models;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;

namespace APDBprojekt.Client.Services
{
    public class StockService : IStockService
    {
        private readonly HttpClient _httpSender;
        private readonly string _key = "mWIWpuj0izHoPFLlryveKnTSWF5OF_tm";

        public StockService(HttpClient httpClient)
        {
            _httpSender = httpClient;
            _httpSender.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ChartData>> GetCompanyStocks(string startDate, string endDate, string ticker)
        {
            var url = $"https://api.polygon.io/v2/aggs/ticker/{ticker}/range/1/day/{startDate}/{endDate}?adjusted=true&sort=asc&limit=120&apiKey={_key}";
            try
            {
                var response = await _httpSender.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var stocks = JObject
                    .Parse(await response.Content.ReadAsStringAsync())
                    .SelectToken("results")
                    .ToObject<List<ChartData>>();
                foreach (var stock in stocks)
                {
                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    dateTime = dateTime.AddMilliseconds(stock.time);
                    stock.Date = dateTime;
                }
                return stocks;
            }
            catch (HttpRequestException) { return new List<ChartData>(); }
            catch (ArgumentNullException) { return new List<ChartData>(); }
        }
    }
}
