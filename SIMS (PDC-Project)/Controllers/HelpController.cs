using System.Threading.Tasks;
using System.Web.Mvc;
//using Supabase_Example.Models;
using Supabase_Example.Services;
using System.Collections.Generic;
using SIMS__PDC_Project_.Models;

namespace Supabase_Example.Controllers
{
    public class StudentController : Controller
    {
        private readonly Supabase_DB_Services _supabaseClient;

        public StudentController()
        {
            _supabaseClient = new Supabase_DB_Services();
        }

        // ✅ GET: /Student/Index
        public async Task<ActionResult> Index()
        {
            var (students, error) = await _supabaseClient.GetAsync<Student>("Student");

            if (!string.IsNullOrEmpty(error))
                TempData["ErrorMessage"] = "Error fetching students: " + error;

            return View(students ?? new List<Student>());
        }

        // ✅ GET: /Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // ✅ POST: /Student/Create
        [HttpPost]
        public async Task<ActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            var (success, error) = await _supabaseClient.AddAsync(student, "Student");

            if (!success)
            {
                TempData["ErrorMessage"] = "Error adding student: " + error;
                return View(student);
            }

            TempData["Message"] = "Student added successfully!";
            return RedirectToAction("Index");
        }

        // ✅ GET: /Student/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var (students, error) = await _supabaseClient.GetAsync<Student>("Student");

            if (!string.IsNullOrEmpty(error))
            {
                TempData["ErrorMessage"] = "Error loading student: " + error;
                return RedirectToAction("Index");
            }

            var student = students?.Find(s => s.id == id);
            if (student == null)
            {
                TempData["ErrorMessage"] = "Student not found.";
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // ✅ POST: /Student/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(Student student)
        {
            var (success, error) = await _supabaseClient.UpdateAsync("Student", "id", student.id.ToString(), student);

            if (!success)
            {
                TempData["ErrorMessage"] = "Error updating student: " + error;
                return View(student);
            }

            TempData["Message"] = "Student updated successfully!";
            return RedirectToAction("Index");
        }

        // ✅ GET: /Student/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var (students, error) = await _supabaseClient.GetAsync<Student>("Student");

            if (!string.IsNullOrEmpty(error))
            {
                TempData["ErrorMessage"] = "Error loading student: " + error;
                return RedirectToAction("Index");
            }

            var student = students?.Find(s => s.id == id);
            if (student == null)
            {
                TempData["ErrorMessage"] = "Student not found.";
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // ✅ POST: /Student/DeleteConfirmed/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var (success, error) = await _supabaseClient.DeleteAsync("Student", "id", id.ToString());

            if (!success)
            {
                TempData["ErrorMessage"] = "Error deleting student: " + error;
                return RedirectToAction("Delete", new { id });
            }

            TempData["Message"] = "Student deleted successfully!";
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Details(int id)
        {
            var (student, error) = await _supabaseClient.GetByIdAsync<Student>("student", "student_id", id.ToString());

            if (!string.IsNullOrEmpty(error))
            {
                TempData["ErrorMessage"] = "Error fetching student: " + error;
                return RedirectToAction("AdminDashboard", "Admin"); // Or wherever your fallback page is
            }

            return View(student);  // Pass the student object to the view
        }
    }

}
