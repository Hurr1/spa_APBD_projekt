using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace APDBprojekt.Shared.Models
{
    public class ChartData
    {
        [JsonProperty("c")] public double Close { get; set; }
        [JsonProperty("h")] public double High { get; set; }
        [JsonProperty("l")] public double Low { get; set; }
        [JsonProperty("o")] public double Open { get; set; }
        [JsonProperty("v")] public double Volume { get; set; }
        [JsonProperty("t")] public double time { get; set; }
        
        public DateTime Date { get; set; }

        
    }
}
