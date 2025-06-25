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
        private readonly SupabaseDbService _dbService = new SupabaseDbService();

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
        public ActionResult Login(string username, string pass)
        {
            string Username = username;
            string Password = pass;

            if (string.IsNullOrWhiteSpace(Username))
            {
                ModelState.AddModelError("username", "Username is required.");
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                ModelState.AddModelError("pass", "Password is required.");
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
        public async Task<ActionResult> StudentSignup(Student s)
        {
            string query = @"INSERT INTO student (name, arid_no, email, password, campus_id, phone_no)
                            VALUES (@name, @aridNo, @email, @pass, @campusId, @phoneNo)";

            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@name", s.name),
                new NpgsqlParameter("@aridNo", s.aridNo),
                new NpgsqlParameter("@email", s.email),
                new NpgsqlParameter("@pass", s.pass),
                new NpgsqlParameter("@campusId", s.campus),
                new NpgsqlParameter("@phoneNo", s.phoneNo),
            };

            var (success, errorMessage) = await _dbService.InsertUpdateDelete(query, parameters);

            if (success)
            {
                TempData["Message"] = "User added successfully!";
                return RedirectToAction("Login");
            }

            TempData["Message"] = "Error adding user: " + errorMessage;
            return View(s);
        }


        // Advisor Signup
        [HttpGet]
        public ActionResult AdvisorSignup()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AdvisorSignup(Advisor a)
        {
            string query = @"INSERT INTO advisor (name, email, password, campus_id, phone_no)
                            VALUES (@name, @email, @pass, @campusId, @phoneNo)";

            var parameters = new List<NpgsqlParameter>
            {
                new NpgsqlParameter("@name", a.name),
                new NpgsqlParameter("@email", a.email),
                new NpgsqlParameter("@pass", a.pass),
                new NpgsqlParameter("@campusId", a.campus),
                new NpgsqlParameter("@phoneNo", a.phoneNo),
            };

            var (success, errorMessage) = await _dbService.InsertUpdateDelete(query, parameters);

            if (success)
            {
                TempData["Message"] = "User added successfully!";
                return RedirectToAction("Login"); // or wherever you want to go
            }

            TempData["Message"] = "Error adding user: " + errorMessage;
            return View(a);
        }

    }


}