using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SIMS__PDC_Project_.Models;
using Supabase_Example.Services;

namespace SIMS__PDC_Project_.Controllers
{
    public class StudentController : Controller
    {
        private readonly Supabase_DB_Services _supabaseClient = new Supabase_DB_Services();

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Equipments()
        {
            var (equipments, error) = await _supabaseClient.GetAsync<Equipment>("equipment");

            if (!string.IsNullOrEmpty(error))
                TempData["EquipmentsErrorMessage"] = "Error fetching Equipments: " + error;

            return View(equipments);
        }

    }
}