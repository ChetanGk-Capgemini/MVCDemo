using MVC_Demo.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Demo.Controllers
{
    public class DashBoardController : Controller
    {

        // GET: DashBoard
        public ActionResult Index()
        {
            dynamic expando = new ExpandoObject();
            //ViewBag.PatientCount=db.Patients.Count();
            //ViewBag.DoctorCount = db.Doctors.Count();
            return View(expando);
        }
    }
}