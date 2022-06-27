using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APDBprojekt.Shared.Models;

namespace APDBprojekt.Client.Models
{
    public class CompanyData
    {

        public Company SelectedCompany { get; set; } = new Company();
        public List<ChartData> Stocks { get; set; } = new List<ChartData>();

        public List<News> News { get; set; } = new List<News>();
    }
}
