using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cumulative1http5125.Models;

namespace cumulative1http5125.Controllers
{
    public class StudentController : Controller
    {
        // GET: localhost:xx/Student/List
        public ActionResult List()
        {
            List<Student> Students = new List<Student>();

            StudentDataController Controller = new StudentDataController();
            Students = Controller.ListStudents();

            return View(Students);
        }


        //GET: localhost:xx/Student/Show/{id}

        public ActionResult Show(int id)
        {
            StudentDataController Controller = new StudentDataController();
            Student SelectedStudent = Controller.FindStudent(id);

            return View(SelectedStudent);
        }
    }
}