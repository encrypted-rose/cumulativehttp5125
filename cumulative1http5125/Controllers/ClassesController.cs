using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cumulative1http5125.Models;

namespace cumulative1http5125.Controllers
{

        public class ClassesController : Controller
    {
        // GET: localhost:xx/Classes/List
        public ActionResult List()
        {
            List<Class> Classes = new List<Class>();

            ClassesDataController Controller = new ClassesDataController();
            Classes = Controller.ListClasses();

            return View(Classes);
        }

        //GET: localhost:xx/Classes/Show/{1}

        public ActionResult Show(int id)
        {
            ClassesDataController Controller = new ClassesDataController();
            Class SelectedClass = Controller.FindClass(id);

            return View(SelectedClass);
        }
  }
}
