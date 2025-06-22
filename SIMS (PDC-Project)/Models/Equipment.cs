using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
//using System.Text.Json.Serialization;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Web;
using Newtonsoft.Json;

namespace SIMS__PDC_Project_.Models
{
    public class Equipment
    {
        //[System.Text.Json.Serialization.JsonIgnore]
        [JsonPropertyName("equipment_id")]
        public int id { get; set; }



        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Enter Equipment Name")]
        [Display(Name = "Name")]
        public string name { get; set; }



        [JsonPropertyName("quantity")]
        [Required(ErrorMessage = "Please Enter Equipment Quantity")]
        [Display(Name = "Quantity")]
        public int quantity { get; set; }



        [JsonPropertyName("category")]
        [Required(ErrorMessage = "Please Enter Equipment Category")]
        [Display(Name = "Category")]
        public string category { get; set; }


        //[JsonIgnore]
        [JsonPropertyName("status")]
        //[NotMapped]
        public string Status { get; set; }


        [JsonPropertyName("campus_id")]
        [Required(ErrorMessage = "Please Select your Campus")]
        [Display(Name = "Campus")]
        public int campus_id { get; set; }
    }
}