using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SIMS__PDC_Project_.Models;
using SIMS__PDC_Project_.ViewModel;
using Supabase_Example.Services;

namespace SIMS__PDC_Project_.Controllers
{
    public class AdminController : Controller
    {

        private readonly Supabase_DB_Services _supabaseClient = new Supabase_DB_Services(); 

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> AdminDashboard()
        {
            List<Advisor> AllAdvisors = new List<Advisor>();
            List<Student> AllStudents = new List<Student>();


            var AllUsers = new Student_Teacher();

            string studenterror, advisorError;

            (AllStudents, studenterror) = await _supabaseClient.GetAsync<Student>("student");

            if (!string.IsNullOrEmpty(studenterror))
            {
                TempData["StudentErrorMessage"] = "Error loading student: " + studenterror;
                return View(AllUsers);
                //return RedirectToAction("Index");
            }



            (AllAdvisors, advisorError) = await _supabaseClient.GetAsync<Advisor>("advisor");

            if (!string.IsNullOrEmpty(advisorError))
            {
                TempData["AdvisorErrorMessage"] = "Error loading student: " + advisorError;
                return View(AllUsers);
                //return RedirectToAction("Index");
            }


             AllUsers = new Student_Teacher
            {
                Students = AllStudents,
                Advisors = AllAdvisors
            };

            return View(AllUsers);
        }


        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }






        [HttpGet]
        public ActionResult Delete()
        {
            return View();
        }


    }
}