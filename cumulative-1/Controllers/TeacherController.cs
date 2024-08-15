using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cumulative_1.Models;

namespace cumulative_1.Controllers
{
    public class TeacherController : Controller
    {
        /// <summary>
        /// get a list of teachers from the SQL database using the ListTeachers method listed in the TeacherDataController
        /// </summary>
        /// <param name="SearchKey"></param>
        /// <returns> A List of teachers first and last names</returns>
        // GOAL: have a web page that shows and routes all the teachers
        // GET: Teacher/List?SearchKey={Key} -> A webpage that shows and routes all the teachers
        public ActionResult List(string SearchKey)
        {
            //work with the teacher data controller
            TeacherDataController TeacherDataController = new TeacherDataController();
            //call the list teachers method
            List<Teacher> Teachers = TeacherDataController.ListTeachers(SearchKey);
            //pass along the List<Teacher> to the view
            ViewData["SearchKey"] = SearchKey;

            ///views/Teacher/List.cshtml
            return View(Teachers);
        }

        /// <summary>
        /// shows a view for a single teacher that has been selected based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>a web page on the selected teacher based on their id</returns>
        // GOAL: have a web page that will show an individual teacher based on their id
        // GET: Teacher/Show/{TeacherID} -> a Webpage for a single teacher
        [HttpGet]
        public ActionResult Show(int id) 
        {
            //Teacher Data Controller
            TeacherDataController TeacherDataController = new TeacherDataController();
            //call the find teacher method
            Teacher SelectedTeacher = TeacherDataController.FindTeacher(id);
            //pass along to /views/teacher/show
            return View(SelectedTeacher);
        }

        /// <summary>
        /// Direct the user to a web page to confirm they wish to delete this selected user from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>redirects the user to a web page to confirm the deletion of a teacher</returns>
        //GOAL : Direct the user to a page to confirm that they want to delete this user
        //GET : Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        /// <summary>
        /// delete a user from the database and redirect the user to the list page
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the user to the list page and deletes the confirmed teacher from the SQL database</returns>
        //GOAL : Delete the user from the SQL database
        //POST : /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        /// <summary>
        /// directs the user to the new teacher page where they can input details about the new teacher
        /// </summary>
        /// <returns>directs the user to the new teacher form page</returns>
        //GOAL : Direct the user to a form page where they can input details about a new teacher to add to the database
        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        /// <summary>
        /// takes the inputted form details and sends this data to the SQL database
        /// </summary>
        /// <param name="teacherfname">string</param>
        /// <param name="teacherlname">string</param>
        /// <param name="employeenumber">string</param>
        /// <param name="salary">decimal</param>
        /// <returns>redirects the user to the list page where they will see the new teacher added to the bottom if everything went correctly</returns>
        //GOAL : Take the user inputted form elements and send the data to the SQL server
        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string teacherfname, string teacherlname, string employeenumber, decimal salary)
        {
            //Create a new teacher object
            Teacher NewTeacher = new Teacher();

            //Identify the inputs provided from the form and set them
            NewTeacher.FirstName = teacherfname;
            NewTeacher.LastName = teacherlname;
            NewTeacher.EmployeeNumber = employeenumber;
            NewTeacher.HireDate = DateTime.Now;
            NewTeacher.Salary = salary;

            //Instantiate a new controller
            TeacherDataController controller = new TeacherDataController();

            //Call the AddTeacher Function to add the user to the SQL database
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        /// <summary>
        /// get data from the database about the current information on a teacher based on id, then change it
        /// </summary>
        /// <returns></returns>
        //GET: /Teacher/Update/{id}
        [HttpGet]
        public ActionResult Update(int id)
        {
            //Teacher Data Controller
            TeacherDataController TeacherDataController = new TeacherDataController();
            //call the find teacher method
            Teacher SelectedTeacher = TeacherDataController.FindTeacher(id);
            return View(SelectedTeacher);
        }

        /// <summary>
        /// a method to send data changes to the SQL database and change them accordingly
        /// </summary>
        /// <param name="id">teacher id</param>
        /// <param name="teacherfname">teacher first name</param>
        /// <param name="teacherlname">teacher last name</param>
        /// <param name="employeenumber">the teacher's employee number</param>
        /// <param name="hiredate">date of their hiring</param>
        /// <param name="salary">the teacher's salary</param>
        /// <returns>a webpage that shows the current information about the teacher</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY
        /// {
        /// "teacherfname": "Marcus",
        /// "teacherlname": "Jeong",
        /// "employeenumber": T123,
        /// "hiredate": "2024-08-12",
        /// "salary": 100.00
        /// }
        /// </example>
        //POST: /Teacher/Update/{id}
        [HttpPost]
        public ActionResult Update(int id, string teacherfname, string teacherlname, string employeenumber, DateTime hiredate, decimal salary)
        {
            //Create a new teacher object
            Teacher TeacherInfo = new Teacher();

            //Identify the inputs provided from the form and set them
            TeacherInfo.FirstName = teacherfname;
            TeacherInfo.LastName = teacherlname;
            TeacherInfo.EmployeeNumber = employeenumber;
            TeacherInfo.HireDate = DateTime.Now;
            TeacherInfo.Salary = salary;

            //Instantiate a new controller
            TeacherDataController controller = new TeacherDataController();

            //Call the UpdateTeacher Function to add the user to the SQL database
            controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/" + id);
        }
    }
}