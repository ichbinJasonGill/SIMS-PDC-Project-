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

        public async Task<ActionResult> AllUsers()
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
                TempData["SuccessMessage"] = $"{type} deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = $"Failed to delete {type}. {error} hjhj";
            }

            //return RedirectToAction("AllUsers");
            return Redirect(Request.UrlReferrer.ToString());
        }


        public async Task<ActionResult> UserRequests()
        {
            List<Advisor> AllAdvisors = new List<Advisor>();
            List<Student> AllStudents = new List<Student>();


            var AllUsers = new Student_Teacher();

            string studenterror, advisorError;

            //(AllStudents, studenterror) = await _supabaseClient.GetAsync<Student>("student");
            (AllStudents, studenterror) = await _supabaseClient.GetByFilterAsync<Student>("student", "status", "pending");


            if (!string.IsNullOrEmpty(studenterror))
            {
                TempData["StudentErrorMessage"] = "Error loading student: " + studenterror;
                return View(AllUsers);
                //return RedirectToAction("Index");
            }



            //(AllAdvisors, advisorError) = await _supabaseClient.GetAsync<Advisor>("advisor");
            (AllAdvisors, advisorError) = await _supabaseClient.GetByFilterAsync<Advisor>("advisor", "status", "pending");

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

        [HttpPost]
        public async Task<ActionResult> UserRequests(int id, string type, string action)
        {
            if (action == "Accept")
            {
                var (success, error) = await _supabaseClient.UpdateAsync(
                type,           // table name
                $"{type}_id",        // column to match
                id.ToString(),               // value to match
                new { status = "accepted" } // only the field to update
                );

                //var (success, error) = await _supabaseClient.UpdateAsync("Student", "id", student.id.ToString(), student);

                if (!success)
                {
                    TempData["ErrorMessage"] = $"Error updating {type}: " + error;
                    return RedirectToAction("UserRequests");
                }

                TempData["AcceptMessage"] = $"{type} Accepted!";
                //return RedirectToAction("UserRequests");
            }
            else if (action == "Reject")
            {
                var (success, error) = await _supabaseClient.UpdateAsync(
                type,           // table name
                $"{type}_id",        // column to match
                id.ToString(),               // value to match
                new { status = "rejected" } // only the field to update
                );

                //var (success, error) = await _supabaseClient.UpdateAsync("Student", "id", student.id.ToString(), student);

                if (!success)
                {
                    TempData["ErrorMessage"] = $"Error updating {type}: " + error;
                    return RedirectToAction("UserRequests");
                }

                TempData["RejectMessage"] = $"{type} Rejected!";
            }
            return RedirectToAction("UserRequests");
        }

        public async Task<ActionResult> Equipments()
        {
            
            var (equipments, error) = await _supabaseClient.GetAsync<Equipment>("equipment");

            if (!string.IsNullOrEmpty(error))
                TempData["EquipmentsErrorMessage"] = "Error fetching Equipments: " + error;

            return View(equipments);
        }

        [HttpGet]
        public ActionResult AddEquipment()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddEquipment(Equipment equipment)
        {
            if (!ModelState.IsValid)
                return View(equipment);

            var (success, error) = await _supabaseClient.AddAsync(equipment, "equipment");
            //var (success, error) = await _supabaseClient.AddAsync("equipment", new
            //{
            //    name = equipment.name,
            //    quantity = equipment.quantity,
            //    category = equipment.category,
            //    Status = equipment.Status,
            //    campus = equipment.campus
            //});

            if (!success)
            {
                TempData["ErrorMessage"] = "Error adding Equipment: " + error;
                return View(equipment);
            }

            TempData["EquipmentSuccessMessage"] = "Equipment added successfully!";
            return RedirectToAction("Equipments");
        }


        [HttpGet]
        public async Task<ActionResult> EditEquipment(string id)
        {

            var (equipment, error) = await _supabaseClient.GetByIdAsync<Equipment>("equipment", "equipment_id", id);

            if (!string.IsNullOrEmpty(error) || equipment == null)
            {
                TempData["ErrorMessage"] = $"Equipment not found.{error}";
                return RedirectToAction("Equipments");
            }

            return View(equipment);
        }

        [HttpPost]
        public async Task<ActionResult> EditEquipment(Equipment updatedEquipment)
        {
            if (!ModelState.IsValid)
            {
                return View(updatedEquipment);
            }

            //var (success, error) = await _supabaseClient.UpdateAsync("equipment", "equipment_id", updatedEquipment.id.ToString(), updatedEquipment);
            var (success, error) = await _supabaseClient.UpdateAsync(
                "equipment",           // table name
                "equipment_id",        // column to match
                updatedEquipment.id.ToString(),               // value to match
                new {
                    name = updatedEquipment.name,
                    quantity = updatedEquipment.quantity,
                    category = updatedEquipment.category,
                    campus_id = updatedEquipment.campus_id
                } // only the field to update
             );

            if (!success)
            {
                TempData["ErrorMessage"] = $"Error updating equipment: {error}";
                return View(updatedEquipment);
            }

            TempData["EquipmentSuccessMessage"] = "Equipment updated successfully!";
            return RedirectToAction("Equipments");
        }

    }
}