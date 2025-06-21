using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
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

            //(AllStudents, studenterror) = await _supabaseClient.GetAsync<Student>("student");
            (AllStudents, studenterror) = await _supabaseClient.GetByFilterAsync<Student>("student","status", "accepted");


            if (!string.IsNullOrEmpty(studenterror))
            {
                TempData["StudentErrorMessage"] = "Error loading student: " + studenterror;
                return View(AllUsers);
                //return RedirectToAction("Index");
            }



            //(AllAdvisors, advisorError) = await _supabaseClient.GetAsync<Advisor>("advisor");
            (AllAdvisors, advisorError) = await _supabaseClient.GetByFilterAsync<Advisor>("advisor", "status", "accepted");

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



        public async Task<ActionResult> GetDeleteModal(int id, string type)
        {
            // Example: fetch name from your data source
            string name = "";
            //if (type == "student")
            //{
            //    var (student, studenterror) = await _supabaseClient.GetByIdAsync<Student>("student", "student_id", id.ToString());
            //    name = student.name;
            //}
            //if (type == "advisor")
            //{
            //    var (advisor, advisorerror) = await _supabaseClient.GetByIdAsync<Advisor>("advisor", "advisor_id", id.ToString());                
            //    name = advisor.name;
            //}

            var (user, advisorerror) = await _supabaseClient.GetByIdAsync<Student>(type, $"{type}_id", id.ToString());
                name = user.name;

            var vm = new ConfirmDelete
            {
                ObjectId = id,
                ObjectName = name,
                ObjectType = type,
                ControllerName = "Admin",
                ActionName = "DeleteConfirmed",
            };

            return PartialView("~/Views/Shared/Partial View/ConfirmDelete.cshtml", vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, string type)
        {

            bool deleted = false;
            string error = null;

            //if (type == "student")
            //{
            //    (deleted, error) = await _supabaseClient.DeleteAsync("student", $"student_id", id.ToString());
            //}
            //else if (type == "advisor")
            //{
            //    (deleted, error) = await _supabaseClient.DeleteAsync("advisor", "advisor_id", id.ToString());
            //}

            (deleted, error) = await _supabaseClient.DeleteAsync(type, $"{type}_id", id.ToString());

            //var (deleted, error) = await _supabaseClient.DeleteAsync(type, "student_id", id.ToString());

            if (deleted)
            {
                TempData["Message"] = $"{type} deleted successfully.";
            }
            else
            {
                TempData["Error"] = $"Failed to delete {type}. {error} hjhj";
            }

            return RedirectToAction("AdminDashboard");
        }



    }
}