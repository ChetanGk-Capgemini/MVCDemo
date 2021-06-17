using MVC_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC_Demo.Controllers
{
    public class OutPatientController : Controller
    {
        string Baseurl = "https://localhost:44354/api/OutPatient/";
        public IEnumerable<Outpatients> GetAllOutpatients()
        {
            IEnumerable<Outpatients> outpatients;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var result = client.GetAsync("Outpatient").Result;
                if (result.IsSuccessStatusCode)
                {
                    outpatients = result.Content.ReadAsAsync<IEnumerable<Outpatients>>().Result;
                }
                else
                {
                    outpatients = Enumerable.Empty<Outpatients>();
                } 
            }
            return outpatients;
        }
        
    
        public ActionResult Index()
        {
            return View(GetAllOutpatients());
        }

        // GET: OutPatient/Details/5
        public ActionResult Details(int id)
        {
            return View(GetAllOutpatients().Where(p=>p.Pid==id).First());
        }

        // GET: OutPatient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OutPatient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pid,Treatment_Date,Doctor_Id")] Outpatients outpatient)
        {
            if (outpatient.Treatment_Date == DateTime.MinValue) outpatient.Treatment_Date = DateTime.Now;
            // TODO: Add insert logic here
            using (HttpClient client = new HttpClient())
           {
               client.BaseAddress = new Uri(Baseurl);
               var postTask = client.PostAsJsonAsync("Outpatient", outpatient);
               var result = postTask.Result;
               if (result.IsSuccessStatusCode)
               {
                   TempData["Outpatient"] = "1";
                   return RedirectToAction("Index");
               }
           }
           return View(outpatient);
        }

        // GET: OutPatient/Edit/5
        public ActionResult Edit(int id)
        {
            
            return View();
        }

        // POST: OutPatient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pid,Treatment_Date,Doctor_Id")] Outpatients outpatient)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var putTask = client.PutAsJsonAsync("Outpatient/" + outpatient.Pid + outpatient.Treatment_Date, outpatient);
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Outpatient"] = "2";
                    return RedirectToAction("Index");
                }
                return View();
            }
        }

        // GET: OutPatient/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OutPatient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, DateTime dt)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var DeleteTask = client.DeleteAsync("Outpatient/Delete/" + id.ToString() + "/" + dt.ToString("yyyy-MM-dd"));
                var result = DeleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Outpatient"] = "3";
                    return RedirectToAction("GetOutPatients");
                }
            }
                return View();
        }
    }
}
