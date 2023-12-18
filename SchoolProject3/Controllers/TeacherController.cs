using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using SchoolProject3.Controllers;
using SchoolProject3.Models;


namespace SchoolProject3.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher/List
        // Go to /Views/Teacher/list.cshtml
        // Browser will open a list of teachers
        public ActionResult List(string TeacherSearchKey = null) // match http request with the method name
        {

            //I have to pass the teacher information to the view
            //I would have to create an instance of the teacher controller 

            TeacherDataController Controller = new TeacherDataController();

            // create a list of objects for listing teachers
            IEnumerable<Teacher> Teachers = Controller.ListTeachers(TeacherSearchKey);

            return View(Teachers);
        }

        // GET: /Teacher/Show/{id}
        // Route to /Views/Teacher/Show.cshtml
        // It will show the details about the teacher
        public ActionResult Show(int id)
        {

            //we want to show a particular Teacher given the id
            TeacherDataController Controller = new TeacherDataController();

            Teacher SelectedTeacher = Controller.FindTeacher(id);
            return View("Show", SelectedTeacher);
        }


        //GET: Teacher/List/TeacherSearchKey={value}
        //Go to /Views/Teacher/List.cshtml
        public ActionResult ListWithSearchKey(string TeacherSearchKey)

        {
            //we need to pass teacher information to the view
            //create an instance of the teacher data controller

            TeacherDataController controller = new TeacherDataController();

            List<Teacher> Teachers = controller.ListTeachers();

            //pass the Teacher information to the /Views/Teacher/List.cshtml

            return View(Teachers);
        }

        //GET: Teacher/New
        //Go to  /Views/Teacher/New.cshtml

        public ActionResult New()
        {
            return View();
        }

        //POST: Teacher/Create
        [HttpPost]

        public ActionResult Create(Teacher newTeacher)
        {
            if (!ModelState.IsValid)
            {
                return View("New");
            }
            //Capture teacher information posted to us

            Debug.WriteLine("I have received teacher name" + newTeacher.teacherfname);

            //actually add the teacher information to the system

            TeacherDataController Controller = new TeacherDataController();
            Controller.AddTeacher(newTeacher);

            //go back to the original list of teachers

            return RedirectToAction("List");
        }

        //GET: /Teacher/DeleteConfirm/{teacherId}
        [HttpGet]
        public ActionResult DeleteConfirm(int id)

        {

            TeacherDataController Controller = new TeacherDataController();
            Teacher SelectedTeacher = Controller.FindTeacher(id);

            return View(SelectedTeacher);
        }

        //POST: /Teacher/Delete/{TeacherId}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Controller.DeleteTeacher(id);

            return RedirectToAction("List");

        }

        //GET: /Teacher/Update/{TeacherId}
        //Goto Views/Teacher/Update.cshtml

        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();

            //I want to display the teacher information
            Teacher SelectedTeacher = controller.FindTeacher(id);   
      
            return View(SelectedTeacher);
        }

        //POST: /Teacher/Edit/{TeacherId}
        //actually updates the teacher
        //redirect to the show teacher page


        /// <summary>
        /// Receives a POST request containing information about an existing teacher in the system, with new values. Conveys this information to the API, and redirects to the "teacher Show" page of our updated teacher.
        /// </summary>
        /// <param name="id">Id of the Teacher to update</param>
        /// <param name="teacherfname">The updated first name of the teacher</param>
        /// <param name="teacherlname">The updated last name of the teacher</param>
        /// <param name="salary">The updated salary of the teacher</param>
        /// <param name="employeenumber">The updated employeenumber of the teacher.</param>
        /// <returns>A dynamic webpage which provides the current information of the teacher.</returns>
        /// <example>
        /// POST : /Teacher/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"teacherfname":"James",
        ///	"teacherlname":"Knowles",
        ///	"salary":"79.67",
        ///	"employeenumber":"T369"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Edit(int id, string teacherfname, string teacherlname, string salary, string employeenumber)
        {

            

            Teacher TeacherInfo = new Teacher();
            TeacherInfo.teacherfname = teacherfname;
            TeacherInfo.teacherlname = teacherlname;
            TeacherInfo.salary = salary;
            TeacherInfo.employeenumber = employeenumber;

            //Teacherfname itself
            Debug.WriteLine("The teacher id is " + id);
            Debug.WriteLine("The teacherfname is " + TeacherInfo.teacherfname);


            //update the Teacher
            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id, TeacherInfo);
            
            //return to the show page
            return RedirectToAction("Show/" + id);
            //confirm we receive the information

            //TeacherId

           


            

            

            
            


        }
    }


}




       