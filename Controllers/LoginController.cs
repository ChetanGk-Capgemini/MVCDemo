using MVC_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Demo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public  ActionResult Index([Bind(Include = "User_Name,Password,RememberMe")] Login login)
        {
            if (!ModelState.IsValid)
            {
                //return View(model);
            }

           
                    return RedirectToAction("Index","DashBoard");
            
        }
        // GET: Login/Register
        public ActionResult Register()
        {
            return View();
        }
    }
}