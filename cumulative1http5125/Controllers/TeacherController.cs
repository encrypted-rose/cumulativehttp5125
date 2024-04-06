using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cumulative1http5125.Models;
using System.Diagnostics;

namespace cumulative1http5125.Controllers
{
    public class TeacherController : Controller
    {
        // GET: localhost:xx/Teacher/List -> returns a page with a list of the teachers in the system

        [HttpGet]
        public ActionResult List(string SearchKey)

        {
            Debug.WriteLine(SearchKey);

            List<Teacher> Teachers = new List<Teacher>();

            TeacherDataController Controller = new TeacherDataController();
            Teachers = Controller.ListTeachers(SearchKey);

            //navigate to Views/Teacher/List.cshtml

            return View(Teachers);
        }

        //Show
        //GET: localhost:xx/Teacher/Show/{id} -> Shows a specific teacher that matches the ID.
        public ActionResult Show(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher SelectedTeacher = Controller.FindTeacher(id);

            return View(SelectedTeacher);
        }


        
        //New

        //GET : localhost:xx/Teacher/New -> Show a new teacher webpage

        public ActionResult New()
        {
            //Leads to Views/Teacher/New.cshtml
            return View();
        }

        //Create

        //POST : localhost:xx/Teacher/Create -> List.cshtml
        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, Decimal Salary)
        {
            //Debugging msg
            //Confirm the information was received
            Debug.WriteLine("Information received");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(HireDate);
            Debug.WriteLine(Salary);

            //Add the teacher in

            TeacherDataController TeacherController = new TeacherDataController();

            Teacher NewTeacher = new Teacher();
            NewTeacher.teacherfname = TeacherFname;
            NewTeacher.teacherlname = TeacherLname;
            NewTeacher.employeenumber = EmployeeNumber;
            NewTeacher.hiredate = HireDate;
            NewTeacher.salary = Salary;

            TeacherController.AddTeacher(NewTeacher);

            //Redirects to List.cshtml
            return RedirectToAction("List");
        }

        //GET: /Teacher/ConfirmDelete/{teacherid} -> a webpage that lets user confirm if they want to delete the entry.
        [HttpGet]
        public ActionResult ConfirmDelete(int id)
        {
            TeacherDataController TeacherController = new TeacherDataController();

            Teacher SelectedTeacher = TeacherController.FindTeacher(id);

            return View(SelectedTeacher);
        }


        //POST: /Teacher/Delete/{id} -> the teacher list page
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherDataController TeacherController = new TeacherDataController();

            TeacherController.DeleteTeacher(id);

            return RedirectToAction("List");
        }
    }

}