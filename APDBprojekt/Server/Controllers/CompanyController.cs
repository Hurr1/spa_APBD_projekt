using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APDBprojekt.Server.Services;
using Newtonsoft.Json.Linq;
using APDBprojekt.Shared.Models;
using System.Text.Json;

namespace APDBprojekt.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository compRepository)
        {
            _companyRepository = compRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody] JObject requestBody)
        {
            var company = await _companyRepository.AddCompany(requestBody);
            return Ok(company);
        }

        [HttpGet("{ticker}/local")]
        public async Task<IActionResult> GetCompanies(string ticker)
        {
            if(ticker == "null_data_ref")
            {
                return Ok(await _companyRepository.GetAllCompanies(""));
            }
            if(ticker == null)
            {
                return BadRequest();
            }    
            return Ok(await _companyRepository.GetAllCompanies(ticker));
        }

        [HttpGet("{ticker}")]
        public async Task<IActionResult> GetCompany(string ticker)
        {
            if (ticker == null)
                return BadRequest();
            var obj = await _companyRepository.GetCompany(ticker);
            if (obj == null)
                return BadRequest();
            obj.Branding = await _companyRepository.GetBranding(obj.BrandingId);
            obj.Address = await _companyRepository.GetAddress(obj.AddressIdAddress);
            return Ok(obj);
        }

        [HttpPut("{ticker}/{username}")]
        public async Task<IActionResult> InsertCompany(string ticker, string username)
        {
            if (ticker == null || username == null)
                return BadRequest();
            await _companyRepository.AddCompanyToUsersList(username, ticker);
            return Ok();
        }

        [HttpGet("{userId}/for")]
        public async Task<IActionResult> GetCompanies(string userId,string smth)
        {
            var companies = await _companyRepository.GetCompaniesForUser(userId);
            return Ok(companies);
        }

        [HttpDelete("{userId}/{ticker}")]
        public async Task<IActionResult> DeleteUsersCompany(string userId, string ticker)
        {
            await _companyRepository.DeleteUsersCompany(userId, ticker);
            return Ok();
        }
    }
}
