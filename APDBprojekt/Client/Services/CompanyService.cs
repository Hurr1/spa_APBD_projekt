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
    public class CompanyService : ICompanyService
    {
        private readonly HttpClient _httpSender;
        private readonly string _key = "mWIWpuj0izHoPFLlryveKnTSWF5OF_tm";

        public CompanyService(HttpClient httpClient)
        {
            _httpSender = httpClient;
            _httpSender.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Company> GetCompany(string ticker)
        {
            var url = $"https://api.polygon.io/v3/reference/tickers/{ticker}?apiKey={_key}";
            try
            {
                var response = await _httpSender.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();    
                await _httpSender.PostAsync("api/company", new StringContent(jsonString, Encoding.UTF8, "application/json"));
                response = await _httpSender.GetAsync($"api/company/{ticker}");
                jsonString = await response.Content.ReadAsStringAsync();
                return JObject.Parse(jsonString).ToObject<Company>();
            }
            catch (HttpRequestException)
            {
                _httpSender.CancelPendingRequests();
                var response = await _httpSender
                                        .GetAsync($"api/company/{ticker}");
                return await HandleException(response);
            }
        }

        public async Task<IEnumerable<CompanyDto>> GetCompaniesByName(string input)
        {
            var url = $"https://api.polygon.io/v3/reference/tickers?search={input}&active=true&sort=ticker&order=asc&apikey={_key}";
            HttpResponseMessage response = null;
            try
            {
                response = await _httpSender.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var companies = JObject
                    .Parse(await response.Content.ReadAsStringAsync())
                    .SelectToken("results")
                    .ToObject<IEnumerable<CompanyDto>>();
                if(!companies.Any())
                {
                    if(input.Length > 0)
                        response = await _httpSender.GetAsync($"api/company/{input}/local");
                    else
                        response = await _httpSender.GetAsync($"api/company/null_data_ref/local");
                    response.EnsureSuccessStatusCode();
                    var jsonString = await response.Content.ReadAsStringAsync();
                    JArray jsonArray = JArray.Parse(jsonString);
                    return JsonConvert.DeserializeObject<IEnumerable<CompanyDto>>(jsonArray /* <-- Here */.ToString());
                }
                return companies;
            }
            catch(HttpRequestException)
            {
                if (input.Length > 0)
                    response = await _httpSender.GetAsync($"api/company/{input}/local");
                else
                    response = await _httpSender.GetAsync($"api/company/null_data_ref/local");
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                JArray jsonArray = JArray.Parse(jsonString);
                return JsonConvert.DeserializeObject<IEnumerable<CompanyDto>>(jsonArray.ToString());
            }
        }



        public async Task AddCompanyToUser(string ticker, string userId)
        {
                var uriApi = $"api/company/{ticker}/{userId}";
                await _httpSender.PutAsync(uriApi, new StringContent("", Encoding.UTF8, "application/json"));
        }

        public async Task<IEnumerable<Company>> GetUsersCompanies(string userId)
        {
            try
            {
                var response = await _httpSender.GetAsync($"api/company/{userId}/for");
                response.EnsureSuccessStatusCode();
                var jsonString = await response.Content.ReadAsStringAsync();
                JArray jsonArray = JArray.Parse(jsonString);
                return JsonConvert.DeserializeObject<IEnumerable<Company>>(jsonArray.ToString());
            }
            catch (HttpRequestException)
            {
                return new List<Company>(); 
            }
        }

        public async Task DeleteUsersCompany(string ticker, string userId)
        {
            var uriApi = $"api/company/{userId}/{ticker}";
            var response = await _httpSender.DeleteAsync(uriApi);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Company> HandleException(HttpResponseMessage response)
        {
            try
            {
                response.EnsureSuccessStatusCode();
                var resString = await response.Content.ReadAsStringAsync();
                return JObject.Parse(resString).ToObject<Company>();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
