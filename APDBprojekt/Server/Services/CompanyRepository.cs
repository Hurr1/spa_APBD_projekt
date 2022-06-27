using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using APDBprojekt.Server.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using APDBprojekt.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace APDBprojekt.Server.Services
{
    public class CompanyRepository : ICompanyRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public CompanyRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Company> AddCompany(JObject company_1)
        {
            var jsonString = JsonConvert.SerializeObject(company_1);
            var company = JObject.Parse(jsonString).SelectToken("results").ToObject<Company>();
            if (company != null)
            {
                if (!_dbContext.Companies.Any(c => c.Ticker == company.Ticker))
                {
                    await _dbContext.Companies.AddAsync(company);
                    await _dbContext.SaveChangesAsync();
                }
            }
            return company;
        }

        public async Task<Company> GetCompany(string ticker)
        {
            var company = await _dbContext.Companies
                .FirstOrDefaultAsync(m => m.Ticker == ticker) ?? null;
            return company;
        }
        public async Task<Address> GetAddress(int id)
        {
            return await _dbContext.Addresses
                .FirstOrDefaultAsync(m => m.IdAddress == id) ?? null;

        }
        public async Task<Branding> GetBranding(int id)
        {
            return await _dbContext.Brandings
                .FirstOrDefaultAsync(m => m.BrandingId == id) ?? null;
        }

        public async Task AddCompanyToUsersList(string userId, string ticker)
        {
            var company = await _dbContext.Companies
                .Where(x => x.Ticker == ticker)
                .FirstOrDefaultAsync();

            var toCheck = await _dbContext.UserCompanies
                .Where(x => x.Id == userId && x.IdCompanyNavigation.Ticker == ticker)
                .SingleOrDefaultAsync();
            if(toCheck == null)
            {
                await _dbContext.UserCompanies.AddAsync(new ApplicationUserCompany { Id = userId, IdCompany = company.CompanyId, IdCompanyNavigation = company });
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Company>> GetCompaniesForUser(string userId)
        {
            var usersCompanies = await _dbContext.UserCompanies
                .Where(x => x.Id == userId)
                .Select(x => new Company
                {
                    Name = x.IdCompanyNavigation.Name,
                    Ticker = x.IdCompanyNavigation.Ticker,
                    Location = x.IdCompanyNavigation.Location
                }).ToListAsync();

            return usersCompanies;
        }

        public async Task DeleteUsersCompany(string userId, string ticker)
        {
            var record = await _dbContext.UserCompanies
                .Where(x => x.Id == userId && x.IdCompanyNavigation.Ticker == ticker).SingleOrDefaultAsync();

            _dbContext.UserCompanies.Remove(record);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Company>> GetAllCompanies(string ticker)
        {
            return await _dbContext.Companies.Include(x => x.Branding).Where(x =>x.Ticker.Contains(ticker)).ToListAsync();
        }
    }
}
