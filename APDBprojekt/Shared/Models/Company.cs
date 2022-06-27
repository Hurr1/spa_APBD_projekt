using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using APDBprojekt.Shared.Models;


namespace APDBprojekt.Shared.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        
        public string Ticker { get; set; }
        
        public string Name { get; set; }

        [JsonProperty("locale")] public string Location { get; set; }

        public Branding Branding { get; set; }

        public Address Address { get; set; }

        public int BrandingId { get; set; }

        public int AddressIdAddress { get; set; }

        public Company()
        {
            Address = new Address();
            Branding = new Branding();
        }
    }
}
