using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cumulative_1.Models
{
    public class Teacher
    {
        //Use this calss to describe what a Teacher is
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public string Course { get; set; }
        public string ClassName { get; set; }
    }
}