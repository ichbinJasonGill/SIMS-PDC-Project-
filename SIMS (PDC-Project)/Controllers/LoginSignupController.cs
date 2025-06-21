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
        public ActionResult Login(string username, string pass, string name)
        {
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

            return RedirectToAction("Success");
        }

        // Student and Advisor Signup
        public ActionResult Signup()
        {
            return View();
        }

        // Student Signup
        [HttpGet]
        public ActionResult StudentSignup()
        {
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
                TempData["Message"] = "Error adding student: " + error;
                return View();
            }

            TempData["Message"] = "Student added successfully!";
            return RedirectToAction("Login");
        }


        // Advisor Signup
        [HttpGet]
        public ActionResult AdvisorSignup()
        {
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
                TempData["Message"] = "Error adding advisor: " + error;
                return View();
            }

            TempData["Message"] = "Advisor added successfully!";
            return RedirectToAction("Login");
        }
    }
}