using System;
using System.Collections.Generic;
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
            //pass along to /views/article/show
            return View(SelectedTeacher);
        }
    }
}