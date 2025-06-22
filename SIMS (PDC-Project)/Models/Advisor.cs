using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;
using System.Web.Mvc;

namespace SIMS__PDC_Project_.Models
{
    public class Advisor
    {
        [JsonIgnore]
        [JsonPropertyName("advisor_id")]
        public int id { get; set; }


        [JsonPropertyName("name")]
        [Required(ErrorMessage = "Please Enter Your Name")]
        [Display(Name = "Full Name")]
        public string name { get; set; }

        [JsonPropertyName("phone_no")]
        [Required(ErrorMessage = "Please Enter Pnone No")]
        [Display(Name = "Phone No")]
        public string phoneNo { get; set; }

        [JsonPropertyName("email")]
        [EmailAddress]
        [Required(ErrorMessage = "Please Enter Email")]
        [Display(Name = "Email Address")]
        public string email { get; set; }

        [JsonPropertyName("password")]
        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Password")]
        public string pass { get; set; }

        [JsonPropertyName("status")]
        [NotMapped]
        [JsonIgnore]
        public string reqStatus { get; set; }

        [JsonPropertyName("campus_id")]
        [Required(ErrorMessage = "Please Select your Campus")]
        [Display(Name = "Campus")]
        public int campus { get; set; }
    }
}