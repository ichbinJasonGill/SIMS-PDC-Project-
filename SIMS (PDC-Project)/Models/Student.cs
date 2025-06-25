using System;
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
    public class Student
    {

        [JsonProperty("student_id")]
        public int id { get; set; }

        [JsonProperty("name")]
        [Required(ErrorMessage = "Please Enter Your Name")]
        [Display(Name = "Full Name")]
        public string name { get; set; }


        [JsonPropertyName("arid_no")]
        //[JsonProperty("arid_no")]
        [Required(ErrorMessage = "Please Enter your Arid NO")]
        [Display(Name = "ARID Number")]
        public string aridNo { get; set; }


        [JsonPropertyName("phone_no")]
        //[JsonProperty("phone_no")]
        [Required(ErrorMessage = "Please Enter Pnone No")]
        [Display(Name = "Phone Nubmer")]
        public string phoneNo { get; set; }

        [JsonProperty("email")]
        [EmailAddress]
        [Required(ErrorMessage = "Please Enter Email")]
        [Display(Name = "Email Address")]
        public string email { get; set; }


        [JsonProperty("password")]
        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Password")]
        public string pass { get; set; }



        [JsonProperty("status")]
        public string reqStatus { get; set; }


        [JsonPropertyName("campus_id")]
        //[JsonProperty("campus_id")]
        [Required(ErrorMessage = "Please Select your Campus")]
        [Display(Name = "Campus")]
        public int campus { get; set; }

    }
}