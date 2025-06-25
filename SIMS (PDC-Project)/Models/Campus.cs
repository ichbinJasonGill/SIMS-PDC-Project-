using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;

namespace SIMS__PDC_Project_.Models
{
	public class Campus
	{
        [JsonPropertyName("campus_id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("location")]
        public string location { get; set; }
    }
}