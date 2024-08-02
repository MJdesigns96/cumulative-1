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

        // GOAL: have a web page that will show an individual teacher based on their id
        // GET: Teacher/Show/{TeacherID} -> a Webpage for a single teacher
        [HttpGet]
        public ActionResult Show(int id) 
        {
            //Article Data Controller
            TeacherDataController TeacherDataController = new TeacherDataController();
            //call the find teacher method
            Teacher SelectedTeacher = TeacherDataController.FindTeacher(id);
            //pass along to /views/teacher/show
            return View(SelectedTeacher);
        }

        //GOAL : Direct the user to a page to confirm that they want to delete this user
        //GET : Teacher/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            return View(NewTeacher);
        }

        //GOAL : Delete the user from the SQL database
        //POST : /Teacher/Delete/{id}
        public ActionResult Delete(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GOAL : Direct the user to a form page where they can input details about a new teacher to add to the database
        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

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
    }
}