using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace APDBprojekt.Shared.Models
{
    public class Address
    {
        [Key]
        public int IdAddress { get; set; }
        [JsonProperty("adress1")]
        
        public string Place { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [JsonProperty("postal_code")]
        public string Postal { get; set; }
    }
}
