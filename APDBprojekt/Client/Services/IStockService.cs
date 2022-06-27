using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APDBprojekt.Shared.Models;

namespace APDBprojekt.Client.Services
{
    public interface IStockService
    {
        public Task<List<ChartData>> GetCompanyStocks(string startDate, string endDate, string ticker);
    }
}
