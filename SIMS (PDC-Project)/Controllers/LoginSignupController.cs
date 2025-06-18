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

        //private readonly SupabaseService _supabaseService = new SupabaseService();



        // GET: LoginSignup
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult demo()
        {
            return View("myModel2");
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

            // Save to database or do something else

            return RedirectToAction("Success");


            //return View();
        }



        public ActionResult Signup()
        {
            return View();
        }






        [HttpGet]
        public ActionResult AdvisorSignup()
        {
            var Advisor = new Advisor
            {
                campus = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Main Campus", Value = "Main Campus" },
                    new SelectListItem { Text = "City Campus", Value = "City Campus" }
                }
            };

            return View(Advisor);
        }

        [HttpPost]
        public ActionResult AdvisorSignup(Advisor a)
        {
            return View();
        }


        [HttpGet]
        public ActionResult StudentSignup()
        {
            var Student = new Student
            {
                CampusOptions = new List<SelectListItem>
                {
                    new SelectListItem { Text = "Main Campus", Value = "Main Campus" },
                    new SelectListItem { Text = "City Campus", Value = "City Campus" }
                },

                status = "Pending"
            };

            return View(Student);
        }

        private readonly SupabaseDbService _dbService = new SupabaseDbService();

        [HttpPost]
        
        public async Task<ActionResult> StudentSignup(Student s)
        {
            string query = $"INSERT INTO \"User\" (name, ARIDno, PhoneNo, Email, Password, Status, Campus) VALUES ('{s.name}', '{s.ARIDno}', '{s.phoneNo}', '{s.email}', '{s.password}', '{s.status}', '{s.Campus}');";
            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@name", s.name),
                new NpgsqlParameter("@age", s.ARIDno),
                new NpgsqlParameter("@age", s.phoneNo),
                new NpgsqlParameter("@age", s.email),
                new NpgsqlParameter("@age", s.password),
                new NpgsqlParameter("@age", s.status),
                new NpgsqlParameter("@age", s.Campus),
            };
            var success = await _dbService.InsertUpdateDelete(query, parameters);
            if (success)
            {
                TempData["Message"] = "User added successfully!";
                //TempData["Message"] = "User added successfully!";
                //return RedirectToAction("UserInsert");s
            }

            ViewBag.Error = "Error adding user.";
            return View(s);
            // Refill dropdown if needed
            //s.CampusOptions = new List<SelectListItem>
            //{
            //    new SelectListItem { Text = "Main Campus", Value = "Main Campus" },
            //    new SelectListItem { Text = "City Campus", Value = "City Campus" }
            //};

            //return View(s);
        }


    }


}