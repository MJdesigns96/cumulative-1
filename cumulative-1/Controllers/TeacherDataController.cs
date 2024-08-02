using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cumulative_1.Models;
using MySql.Data.MySqlClient;

namespace cumulative_1.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        //This controller will let us access the teacher's data form the db
        /// <summary>
        /// Returns a list of Teachers in the database
        /// </summary>
        /// <example> GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of teachers (first and last names)
        /// </returns>

        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey}")]
        public List<Teacher> ListTeachers(string SearchKey)
        {
            //create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and database
            Conn.Open();

            //establich a new command query for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from teachers where teacherfname like lower(@key)" +
                " OR teacherlname like lower(@key)" +
                "or lower(concat(teacherfname,' ', teacherlname)) like lower(@key)";

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher's Names
            List<Teacher> TeachersNames = new List<Teacher>();

            //Loop through each row of the result set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();

                //Create a Teacher Object
                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherID = TeacherId;
                NewTeacher.FirstName = TeacherFName;
                NewTeacher.LastName = TeacherLName;
                //add name to the list
                TeachersNames.Add(NewTeacher);
            }

            //Close the connection between the MySQL db and the WebServer
            Conn.Close();

            //return the final list of teacher's names
            return TeachersNames;
        }

        /// <summary>
        /// Find a teacher that matches the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// a single teacher and their information
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher")]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //open the connection between the web server and database
            Conn.Open();

            //establich a new command query for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from teachers left JOIN classes ON teachers.teacherid = classes.teacherid where teachers.teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Loop through each row of the result set
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFName = ResultSet["teacherfname"].ToString();
                string TeacherLName = ResultSet["teacherlname"].ToString();
                string TeacherNumber = ResultSet["employeenumber"].ToString();
                DateTime TeacherHireDate = (DateTime)ResultSet["hiredate"];
                decimal TeacherSalary = (decimal)ResultSet["salary"];
                string ClassCode = ResultSet["classcode"].ToString();
                string ClassName = ResultSet["classname"].ToString();

                //Create a Teacher Object
                NewTeacher.TeacherID = TeacherId;
                NewTeacher.FirstName = TeacherFName;
                NewTeacher.LastName = TeacherLName;
                NewTeacher.EmployeeNumber = TeacherNumber;
                NewTeacher.HireDate = TeacherHireDate;
                NewTeacher.Salary = TeacherSalary;
                NewTeacher.Course = ClassCode;
                NewTeacher.ClassName = ClassName;
            }

            //Close the connection between the MySQL db and the WebServer
            Conn.Close();

            //return the final list of teacher's names
            return NewTeacher;
        }

        /// <summary>
        /// Deletes a teacher within the SQL database
        /// </summary>
        /// <param name="id"></param>
        /// <example> POST : /api/TeacherDate/DeleteTeacher/3</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection
            Conn.Open();

            //Establish a new query for the database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL text
            cmd.CommandText = "DELETE from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //Close the connection
            Conn.Close();
        }

        /// <summary>
        /// add a new teacher to the database
        /// </summary>
        /// <param name="Teacher"></param>
        [HttpPost]
        public void AddTeacher([FromBody]Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection
            Conn.Open();

            //Establish a new query for the database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL text
            cmd.CommandText = "insert into teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFname, @TeacherLname,@EmployeeNumber,@HireDate,@Salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.FirstName);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.LastName);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            //Close the connection
            Conn.Close();
        }
    }
}
