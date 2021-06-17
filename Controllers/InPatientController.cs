using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVC_Demo.Models;

namespace MVC_Demo.Controllers
{
   
    public class InPatientController : Controller
    {
        string Baseurl = "https://localhost:44354/api/";

        public IEnumerable<Inpatients> GetAllInPatients()
        {
            IEnumerable<Inpatients> inpatients = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {

                    client.BaseAddress = new Uri(Baseurl);
                    var result = client.GetAsync("InPatient").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        inpatients = result.Content.ReadAsAsync<IList<Inpatients>>().Result;
                        TempData["InPatient"] = "1";
                    }
                }
            }
            catch
            {
                inpatients = Enumerable.Empty<Inpatients>();
                TempData["InPatient"] = "-1";
            }


            return inpatients;
        }
        public ActionResult Index()
        {
            return View(GetAllInPatients());
        }
        public ActionResult Details()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pid,Room_No,Doctor_Id,Admission_Date")] Inpatients inpatients)
        {
            return View(inpatients);
        }
        public ActionResult Edit(int id,DateTime admissionDate)
        {
            IEnumerable<Inpatients> inpatients = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var result = client.GetAsync("SearchInpatient/"+id+admissionDate).Result;
                if (result.IsSuccessStatusCode)
                {
                    inpatients = result.Content.ReadAsAsync<IList<Inpatients>>().Result;
                }
                else
                {
                    inpatients = Enumerable.Empty<Inpatients>();
                }
            }
            Inpatients IP = inpatients.Where(i => i.Pid == id).First();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pid,Room_No,Doctor_Id,Admission_Date,Discharge_Date")] Inpatients inpatients)
        {
            IEnumerable<Inpatients> inpatient = null;
            try {
                
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    var result = client.GetAsync("SearchInpatient/" + 1 + DateTime.Now).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        inpatient = result.Content.ReadAsAsync<IList<Inpatients>>().Result;
                    }
                    else
                    {
                        inpatient = Enumerable.Empty<Inpatients>();
                    }
                }
                Inpatients IP = inpatient.Where(i => i.Pid == 1).First();
                return View();
            }
            catch
            {
                TempData["InPatient"] = "-1";
                return RedirectToAction("Index");
            }
            return View(inpatients);
        }
    }
}