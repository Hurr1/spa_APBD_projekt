using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace APDBprojekt.Server.Models
{
    public class Branding
    {
        [Key]
        public int BrandingId { get; set; }
        [JsonProperty("logo_url")] public string Logo { get; set; }
        [JsonProperty("icon_url")] public string Icon { get; set; }

    }
}
