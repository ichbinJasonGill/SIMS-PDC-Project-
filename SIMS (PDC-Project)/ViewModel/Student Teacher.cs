using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIMS__PDC_Project_.Models;

namespace SIMS__PDC_Project_.ViewModel
{
    public class Student_Teacher
    {
        public List<Student> Students { get; set; }
        public List<Advisor> Advisors { get; set; }
    }
}