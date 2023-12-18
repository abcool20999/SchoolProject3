using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolProject3.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using Org.BouncyCastle.Bcpg;
using System.Web.Routing;


namespace SchoolProject3.Controllers
{
    public class TeacherDataController : ApiController
    {
        ///The database context class which will allow us to access our MySql Database.

        private SchoolDbContext School = new SchoolDbContext();

        ///This controller will access the teachers table of the school database.
        /// <summary>
        /// Returns a list of Teachers first and lastname in the system
        /// </summary>
        /// <returns>
        /// A list of teachers objects
        /// </returns>// This controller will access the teachers table of our school database
        /// <example>
        /// GET api/TeacherData/ListTeachers --> ["employeenumber":"T321", "hiredate":"2016-08-05T00:00:00", "salary":"55.30",
        ///                                       "teacherId":"1", "teacherfname":"Alexander", "teacherlname":"Bennet"]
        /// </example>



        [HttpGet]
        [Route("api/TeacherData/ListTeachers")]
        public List<Teacher> ListTeachers()
        {
            // Create a list to store the teachers
            List<Teacher> Teachers = new List<Teacher>();

            // Installed the MySQL.NET--MySql.Data Connector via NuGet Package Manager
            // Create the connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open the connection
            Conn.Open();

            // Create command
            MySqlCommand cmd = Conn.CreateCommand();

            // Set the SQL command
            cmd.CommandText = "Select * from teachers";

            // Get result set and create a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            // Loop through the list
            while (ResultSet.Read())
            {
                // Create a new Teacher object for each record
                Teacher newTeacher = new Teacher();

                // Access Column information by the DB column name as an index

                // Get the teacher id
                newTeacher.teacherId = Convert.ToInt32(ResultSet["teacherid"]);

                // Get the teacher's firstname
                newTeacher.teacherfname = ResultSet["teacherfname"].ToString();

                // Get the teacher's lastname
                newTeacher.teacherlname = ResultSet["teacherlname"].ToString();

                // Get the hire date
                string HireDate = ResultSet["hiredate"].ToString();
                newTeacher.hiredate = DateTime.Parse(HireDate);

                // Get the employee number
                newTeacher.employeenumber = ResultSet["employeenumber"].ToString();

                // Get the salary
                newTeacher.salary = ResultSet["salary"].ToString();

                // Add the Teacher object to the list
                Teachers.Add(newTeacher);
            }

            // Close the connection between the MySQL Database and the WebServer 
            Conn.Close();

            // Return the final list of teacher objects
            return Teachers;
        }

        /// <summary>
        /// Find a Teacher by the input id
        /// </summary>
        /// <param name="TeacherId">the teacherid primary key in the database</param>
        /// <returns>
        /// this returns a teacher object
        /// </returns> 
        /// <example>
        /// GET api/TeacherData/FindTeacher/{TeacherId} -> {"TeacherId":"1","employmeenumber":"T505","hiredate":"2015-10-23T00:00:00",
        ///                                                 "salary":"79.63"}
        /// 
        /// GET api/TeacherData/FindTeacher/{TeacherId} -> {"TeacherId":"4","employmeenumber":"T371","hiredate":"2016-11-23T00:00:00",
        ///                                                 "salary":"50.63"}
        /// </example>

        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{TeacherId}")]
        public Teacher FindTeacher(int TeacherId)
        {

            //Create Connection
            MySqlConnection Conn = School.AccessDatabase();


            //Open Connection
            Conn.Open();


            //Create a command
            MySqlCommand command = Conn.CreateCommand();


            //Set the command text
            command.CommandText = "SELECT teachers.teacherid, teachers.teacherfname, teachers.teacherlname, teachers.employeenumber, teachers.hiredate, teachers.salary, classes.classname FROM classes RIGHT JOIN teachers ON classes.teacherid = teachers.teacherid WHERE teachers.teacherid = " + TeacherId;



            //Result Set
            MySqlDataReader ResultSet = command.ExecuteReader();

            Teacher SelectedTeacher = new Teacher();
            while (ResultSet.Read())
            {
                //Get the teacherId
                SelectedTeacher.teacherId = Convert.ToInt32
                (ResultSet["teacherid"]);


                //Get the teacherfname
                SelectedTeacher.teacherfname = ResultSet["teacherfname"].ToString();

                //Get the teacherlname
                SelectedTeacher.teacherlname = ResultSet["teacherlname"].ToString();


                //Get the employeenumber
                SelectedTeacher.employeenumber = ResultSet["employeenumber"].ToString();

                //Get the hiredate
                SelectedTeacher.hiredate = DateTime.Parse(ResultSet["hiredate"].ToString());

                //Get the salary
                SelectedTeacher.salary = ResultSet["salary"].ToString();

                //Get the classname
                SelectedTeacher.classname = ResultSet["classname"].ToString();




            }
            //Close connection
            Conn.Close();

            return SelectedTeacher;

        }
        /// <summary>
        /// It displays a list of teacher names represented with the searchkey entered in the search textbox.
        /// </summary>
        /// <param name="TeacherSearchKey">The Teachers to search for</param>
        /// <returns>
        /// It returns names of teachers that match the teacher names in the search box
        /// </returns>
        /// <example>
        /// GET api/TeacherData/FindTeacher/{TeacherSearchKey} -> {"TeacherId":"1","employmeenumber":"T505","hiredate":"2015-10-23T00:00:00",
        ///                                                 "salary":"79.63", "teacherfname":"Linda", "teacherlname":"Chan"}
        /// 
        /// </example>


        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{TeacherSearchKey}")]
        public IEnumerable<Teacher> ListTeachers(string TeacherSearchKey)
        {
            Debug.WriteLine("Trying to do an API search for " + TeacherSearchKey);

            // Create Connection
            MySqlConnection Conn = School.AccessDatabase();

            // Open Connection
            Conn.Open();

            // Create a command
            MySqlCommand command = Conn.CreateCommand();

            // Set the command text
            command.CommandText = "SELECT * FROM teachers WHERE LOWER(teacherfname) LIKE LOWER('%" + TeacherSearchKey + "%') OR LOWER(teacherlname) LIKE LOWER('%" + TeacherSearchKey + "%')";


            // Result Set
            MySqlDataReader ResultSet = command.ExecuteReader();

            // Create a list to store the fetched teachers
            List<Teacher> teachers = new List<Teacher>();

            // Loop through the result set
            while (ResultSet.Read())
            {
                // Fetch data and create a Teacher object
                Teacher newTeacher = new Teacher();

                //Get the teacherId
                newTeacher.teacherId = Convert.ToInt32(ResultSet["teacherid"]);

                //Get the employee number
                newTeacher.employeenumber = ResultSet["employeenumber"].ToString();

                //Get the hiredate
                newTeacher.hiredate = DateTime.Parse(ResultSet["hiredate"].ToString());

                //Get the salary
                newTeacher.salary = ResultSet["salary"].ToString();

                //Get the teacher first name
                newTeacher.teacherfname = ResultSet["teacherfname"].ToString();

                //Get the teacher lastname
                newTeacher.teacherlname = ResultSet["teacherlname"].ToString();

                // Add the new Teacher object to the list
                teachers.Add(newTeacher);
            }

            // Close the connection
            Conn.Close();

            // Return the list of teachers
            return teachers;
        }

        //new method for adding a Teacher

        /// <summary>
        /// Receives the teacher information and adds a teacher into the database
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// POST: api/TeacherData/AddTeacher
        /// 
        /// FORM DATA / POST DATA
        /// {
        ///     "teacherfname":"Owen"
        ///     "teacherlname":"Liang"
        /// }
        /// </example>
        /// <example>
        /// curl -d @teacher.json -H "Content-Type: application/json"
        /// http://localhost52959/api/Teacherdata/AddTeacher
        /// </example>
        /// AFTER ADD, DELETE we wil focus on the api directly

        [HttpPost]
        [Route("api/TeacherData/AddTeacher")]
        public void AddTeacher(Teacher newTeacher)
        {
            //assume that the information is received correctly

            //contact the database and execute a query

            //what kind of query would we need to include?

            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();

            string query = "INSERT INTO teachers(teacherfname, teacherlname, employeenumber, hiredate, salary) VALUES(@teacherfname, @teacherlname, @employeenumber, CURRENT_DATE(), CAST(@salary AS DECIMAL(10, 2)))";

            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@teacherfname", newTeacher.teacherfname);
            cmd.Parameters.AddWithValue("@teacherlname", newTeacher.teacherlname);
            cmd.Parameters.AddWithValue("@employeenumber", newTeacher.employeenumber);
            cmd.Parameters.AddWithValue("@salary", newTeacher.salary);
            cmd.Parameters.AddWithValue("@hiredate", newTeacher.hiredate);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
        }

        //new method deleting a Teacher
        /// <summary>
        /// Delete a Teacher in the System
        /// </summary>
        /// <param name="TeacherId">The Teacher Id in the system</param>
        /// <return>
        /// </return>
        /// <example>
        /// POST api/TeacherData/DeleteTeacher/15 --> 
        /// </example>
        [HttpPost]
        [Route("api/TeacherData/DeleteTeacher/{TeacherId}")]
        public void DeleteTeacher(int TeacherId)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            string query = "delete from teachers where teacherid=@teacherid";
            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@teacherid", TeacherId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            Conn.Close();
        }
        /// <summary>
        /// Updates the Teacher information in the database given the Teacher Id and Teacher information
        /// </summary>
        /// <param name="TeacherId">The TeacherId to update</param>
        /// <param name="UpdateTeacher">A Teacher object containing the new information</param>
        /// <example>
        /// curl -d @teacher2.json -H "Content-Type: application/json"
        /// http://localhost52959/api/Teacherdata/UpdateTeacher/Id
        /// </example>
        /// <example>
        /// OPTION 1
        ///
        /// POST api/TeacherData/UpdateTeacher/4
        /// 
        /// POST DATA / FORM DATA / REQUEST BODY
        /// {
        ///    "teacherfname" : "Thomas",
        ///    "teacherlname" : "Frederick"
        /// }
        /// 
        /// OPTION 2
        /// 
        /// POST api/TeacherData/UpdateTeacher
        /// 
        /// POST DATA / FORM DATA / REQUEST BODY
        /// {
        ///    "TeacherId" : "4",
        ///    "teacherfname" : "Thomas",
        ///    "teacherlname" : "Frederick"
        /// }
        /// </example>
        /// <return></return>
        [HttpPost]
        [Route("api/TeacherData/UpdateTeacher/{TeacherId}")]
        public void UpdateTeacher(int TeacherId, [FromBody]Teacher UpdateTeacher) 
        {
            Debug.WriteLine("The API has been reached");

            Debug.WriteLine("The ID is "+TeacherId);

            Debug.WriteLine("The teacher first name is" +UpdateTeacher.teacherfname);

            //build the update logic
            //query

            MySqlConnection conn = School.AccessDatabase();

            conn.Open();
            MySqlCommand cmd = conn.CreateCommand();

           string query = "UPDATE Teachers SET teacherfname = @fname, teacherlname = @lname, " +
                           "salary = @salary, employeenumber = @employeenumber WHERE TeacherId = @id";

            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@fname", UpdateTeacher.teacherfname);
            cmd.Parameters.AddWithValue("@lname", UpdateTeacher.teacherlname);
            cmd.Parameters.AddWithValue("@salary", UpdateTeacher.salary);
            cmd.Parameters.AddWithValue("@employeenumber", UpdateTeacher.employeenumber);
            cmd.Parameters.AddWithValue("@id", TeacherId);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            conn.Close();

            return;

         }
    }
}