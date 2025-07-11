﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SIMS__PDC_Project_.Models
{
    public class Advisor
    {
        //[JsonPropertyName("advisor_id")]
        //[JsonIgnore]
        [JsonProperty("advisor_id")]
        public int id { get; set; }


        [JsonPropertyName("name")]

        //[JsonProperty("name")]
        [Required(ErrorMessage = "Please Enter Your Name")]
        [Display(Name = "Full Name")]
        public string name { get; set; }


        [JsonPropertyName("phone_no")]
        //[JsonProperty("phone_no")]
        [Required(ErrorMessage = "Please Enter Pnone No")]
        [Display(Name = "Phone No")]
        public string phoneNo { get; set; }


        [JsonPropertyName("email")]
        //[JsonProperty("email")]
        [EmailAddress]
        [Required(ErrorMessage = "Please Enter Email")]
        [Display(Name = "Email Address")]
        public string email { get; set; }


        [JsonPropertyName("password")]
        //[JsonProperty("password")]
        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Password")]
        public string pass { get; set; }



        [JsonPropertyName("status")]
        //[JsonProperty("status")]
        [NotMapped]
        //[JsonIgnore]
        public string reqStatus { get; set; }


        [JsonPropertyName("campus_id")]
        //[JsonProperty("campus_id")]
        [Required(ErrorMessage = "Please Select your Campus")]
        [Display(Name = "Campus")]
        public int campus { get; set; }
    }
}