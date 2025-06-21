
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIMS__PDC_Project_.Models;

namespace SIMS__PDC_Project_.ViewModel
{
    public class ConfirmDelete
    {
        public int ObjectId { get; set; }                // ID to delete
        public string ObjectName { get; set; }           // Display name
        public string ObjectType { get; set; }           // e.g. "Advisor", "Student"
        public string ActionName { get; set; }           // e.g. "ConfirmDelete"
        public string ControllerName { get; set; }       // e.g. "Admin"

        //public string CancelAction { get; set; }         // e.g. "AdminDashboard"
        //public string CancelController { get; set; }     // usually same controller
    }
}
