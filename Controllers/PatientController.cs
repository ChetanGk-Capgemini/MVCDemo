using MVC_Demo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC_Demo.Controllers
{
    public class PatientController : Controller
    {
        List<Patients> EmpInfo = new List<Patients>();
        // GET: Patients
        string Baseurl = "https://localhost:44354/api/";
        public IEnumerable<Patients> GetAllPatients()
        {
            
            IEnumerable<Patients> Pat = null;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(Baseurl);
                    var result = client.GetAsync("Patient").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        Pat = result.Content.ReadAsAsync<IEnumerable<Patients>>().Result;
                    }
                    else
                    {
                        TempData["Patient"] = "-1";
                        Pat = Enumerable.Empty<Patients>();
                    }
                }
                catch
                {
                    TempData["Patient"] = "-1";
                    Pat = Enumerable.Empty<Patients>();

                }
            }
            return Pat;
        }

        public ActionResult Index()
        {
            
            return View(GetAllPatients());
        }

        public ActionResult Details(int id)
        {
            Patients patient = GetAllPatients().Where(i => i.Patient_Id == id).First();
            return View(patient);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Age,Weight,Gender,Address,Phone_No,Disease")] Patients patient)
        {
            try
            {
                patient.Patient_Id = GetAllPatients().Max(i => i.Patient_Id) + 1;
                using (HttpClient client = new HttpClient())
                {

                    TempData.Keep();
                    client.BaseAddress = new Uri(Baseurl);
                    var postTask = client.PostAsJsonAsync("Patient", patient);
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Patient"] = "1";
                        return RedirectToAction("Index");
                    }

                }
            }
            catch
            {
                TempData["Patient"] = "-1";
                return RedirectToAction("Index",patient);
            }
            return RedirectToAction("Create",patient);
        }
        public ActionResult Edit(int id)
        {
            TempData["id"] = id;
            Patients patient = GetAllPatients().Where(i => i.Patient_Id == id).First();
            return View(patient);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Name,Age,Weight,Gender,Address,Phone_No,Disease")] Patients patient)
        {
            patient.Patient_Id = (int)TempData.Peek("id");
            
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var putTask = client.PutAsJsonAsync("Patient/" + patient.Patient_Id, patient);
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Patient"] = "2";
                    return RedirectToAction("Index");
                }
                return View(patient);
            }
            
            
        }
        public ActionResult Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var DeleteTask = client.DeleteAsync("Patient/" + id);
                var result = DeleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Patient"] = "3";
                    return RedirectToAction("Index");
                }
            }
            return View();
            
            
        }
    }
}