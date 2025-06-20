using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace SIMS__PDC_Project_.Models
{
    public class Student
    {
        public int id { get; set; }


        [Required(ErrorMessage = "Please Enter Your Name")]
        [Display(Name = "Full Name")]
        public string name { get; set; }


        [Required(ErrorMessage = "Please Enter your Arid NO")]
        [Display(Name = "ARID Number")]
        public string aridNo { get; set; }


        [Required(ErrorMessage = "Please Enter Pnone No")]
        [Display(Name = "Phone Nubmer")]
        public string phoneNo { get; set; }


        [EmailAddress]
        [Required(ErrorMessage = "Please Enter Email")]
        [Display(Name = "Email Address")]
        public string email { get; set; }


        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Password")]
        public string pass { get; set; }

        [NotMapped]
        [JsonIgnore]
        public string reqStatus { get; set; }

        [Required(ErrorMessage = "Please Select your Campus")]
        [Display(Name = "Campus")]
<<<<<<< HEAD
        public string Campus { get; set; }

        [NotMapped]
        [JsonIgnore]
        public List<SelectListItem> CampusOptions { get; set; }

=======
        public int campus { get; set; }
>>>>>>> main
    }
}