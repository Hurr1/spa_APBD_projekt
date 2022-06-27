using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APDBprojekt.Shared.Models;
namespace APDBprojekt.Client.Services
{
    public interface ICompanyService
    {

        public Task<Company> GetCompany(string ticker);

        public Task<IEnumerable<CompanyDto>> GetCompaniesByName(string input);

        public Task<IEnumerable<Company>> GetUsersCompanies(string userId);

        public Task AddCompanyToUser(string ticker,string userId);

        public Task DeleteUsersCompany(string ticker, string userId);

    }
}