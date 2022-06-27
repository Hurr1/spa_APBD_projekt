using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using APDBprojekt.Server.Models;

namespace APDBprojekt.Server.Services
{
    public interface ICompanyRepository
    {
        Task<Company> GetCompany(string ticker);

        Task<IEnumerable<Company>> GetAllCompanies(string ticker);

        Task<Address> GetAddress(int id);

        Task<Branding> GetBranding(int id);

        Task<Company> AddCompany(JObject company);

        Task AddCompanyToUsersList(string userId, string ticker);

        Task<List<Company>> GetCompaniesForUser(string userId);

        Task DeleteUsersCompany(string userId, string ticker);
    }
}
