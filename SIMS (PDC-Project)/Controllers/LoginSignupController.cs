using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Npgsql;
using System.Web.Services;
using SIMS__PDC_Project_.Models;
using Supabase_Example.Services;
using Microsoft.Ajax.Utilities;


namespace SIMS__PDC_Project_.Controllers
{
    public class LoginSignupController : Controller
    {
        private readonly Supabase_DB_Services _supabaseClient;

        public LoginSignupController()
        {
            _supabaseClient = new Supabase_DB_Services();
        }

        // GET: LoginSignup
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Login(string username, string pass, string user)
        {
            // For student
            if (user == "student") 
            {
                var (student, message) = await _supabaseClient.LoginAsync<Student>(
                    "student",
                    "name",
                    username,
                    "password",
                    pass
                );

                
                if (!string.IsNullOrEmpty(message))
                {
                    TempData["Message"] = message;
                    return View("");
                }
                else if (student.reqStatus == "pending")
                {
                    TempData["Message"] = "Your request is pending approval.";
                    return View();
                }
                else if (student.reqStatus == "rejected")
                {
                    TempData["ErrorMessage"] = "Your request has been rejected.";
                    return View();
                }
                else
                {
                    //Session["StudentId"] = student.id; // Store student ID in session
                    //Session["StudentName"] = student.name; // Store student name in session
                    TempData["Message"] = "Student Found";
                    return View();
                }
            }

            // For advisor
            else if (user == "advisor") 
            {
                var (advisor, message) = await _supabaseClient.LoginAsync<Advisor>(
                    "advisor",
                    "name",
                    username,
                    "password",
                    pass
                );


                if (!string.IsNullOrEmpty(message))
                {
                    TempData["ErrorMessage"] = message;
                    return View("");
                }
                else
                {
                    TempData["Message"] = "Advisor Found";
                    return View();
                }
            }

            // For admin
            else if (user == "admin") 
            {
                var (advisor, message) = await _supabaseClient.LoginAsync<Advisor>(
                    "admin",
                    "name",
                    username,
                    "Password",
                    pass
                );


                if (!string.IsNullOrEmpty(message))
                {
                    TempData["ErrorMessage"] = message;
                    return View("");
                }

            }
            string Username = username;
            string Password = pass;

            if (string.IsNullOrWhiteSpace(Username))
            {
                ModelState.AddModelError("username", "Username is required.");
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ModelState.AddModelError("pass", "Email is required.");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            return RedirectToAction("AllUsers", "Admin");
        }

        // Student and Advisor Signup
        public ActionResult Signup()
        {
            return View();
        }

        // Student Signup
        [HttpGet]
        public async Task<ActionResult> StudentSignup()
        {
            var campusData = await GetCampus();
            var campusList = new List<SelectListItem>();

            foreach (var campus in campusData)
            {
                campusList.Add(new SelectListItem
                {
                    Value = campus.id.ToString(),   // Replace 'id' with actual ID property
                    Text = campus.name              // Replace 'name' with actual name property
                });
            }

            ViewBag.CampusList = campusList;

            return View();
        }


        [HttpPost]
        public async Task<ActionResult> StudentSignup(Student student)
        {
            if (!ModelState.IsValid)
                return View();

            var (success, error) = await _supabaseClient.AddAsync(student, "student");

            if (!success)
            {
                TempData["ErrorMessage"] = "Error adding student: " + error;
                return View();
            }

            TempData["Message"] = "Student added successfully!";
            return RedirectToAction("Login");
        }


        // Advisor Signup
        [HttpGet]
        public async Task<ActionResult> AdvisorSignup()
        {
            var campusData = await GetCampus();

            var campusList = new List<SelectListItem>();
            foreach (var campus in campusData)
            {
                campusList.Add(new SelectListItem
                {
                    Value = campus.id.ToString(),   // Replace 'id' with actual ID property
                    Text = campus.name              // Replace 'name' with actual name property
                });
            }
            ViewBag.CampusList = campusList;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AdvisorSignup(Advisor advisor)
        {
            if (!ModelState.IsValid)
                return View();

            var (success, error) = await _supabaseClient.AddAsync(advisor, "advisor");

            if (!success)
            {
                TempData["ErrorMessage"] = "Error adding student: " + error;
                return View();
            }

            TempData["Message"] = "Advisor added successfully!";
            return RedirectToAction("Login");
        }

        public async Task<List<Campus>> GetCampus()
        {
            var (campusList, message) = await _supabaseClient.GetAsync<Campus>("campus");

            if (!string.IsNullOrEmpty(message))
            {
                Console.WriteLine("Error fetching campus data: " + message);
                return new List<Campus>();
            }

            return campusList;
        }

    }
}