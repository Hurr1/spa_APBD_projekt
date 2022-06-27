using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APDBprojekt.Client.Models;
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
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpSender;
        private readonly string _key = "mWIWpuj0izHoPFLlryveKnTSWF5OF_tm";

        public NewsService(HttpClient httpClient)
        {
            _httpSender = httpClient;
            _httpSender.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<List<News>> GetNews(string ticker)
        {
            var url = $"https://api.polygon.io/v2/reference/news?ticker={ticker}&apiKey={_key}";
            try
            {
                var response = await _httpSender.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                return JObject.Parse(jsonString)
                    .SelectToken("results")
                    .ToObject<List<News>>();
            }
            catch (HttpRequestException)
            {
                return new List<News>();
            }
        }
    }
}
