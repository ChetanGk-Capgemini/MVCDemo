using MVC_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace MVC_Demo.Controllers
{
    public class DoctorController : Controller
    {
        // GET: AllDoctor
        private string Baseurl = "https://localhost:44354/api/";

        private IEnumerable<Doctors> doctors = null;

        public IEnumerable<Doctors> GetDoctorsList()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(Baseurl);
                    var result = client.GetAsync("Doctors").Result;
                    if (result.IsSuccessStatusCode)
                    {
                        doctors = result.Content.ReadAsAsync<IList<Doctors>>().Result;
                    }
                    else
                    {
                        doctors = Enumerable.Empty<Doctors>();
                    }
                }
                catch
                {
                    TempData["Doctor"] = "-1";
                    doctors = Enumerable.Empty<Doctors>();
                }
            }
            return doctors;
        }

        //Index
        public ActionResult Index()
        {
            return View(GetDoctorsList());
        }

        //View
        public ActionResult Details(int doctor)
        {
            return View(GetDoctorsList().Where(d => d.Doctor_Id == doctor).First());
        }

        //Create
        public ActionResult Create()
        {
            return View();
        }

        //Create Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DoctorId,Doctor_Name,DeptId")] Doctors doctor)
        {
            try
            {
                doctor.Doctor_Id = GetDoctorsList().Max(d => d.Doctor_Id) + 1;
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);
                    var postTask = client.PostAsJsonAsync("Doctors", doctor);
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Doctor"] = "1";
                        return RedirectToAction("Index");
                    }
                    else
                        return View(doctor);
                }
            }
            catch 
            {
                TempData["Doctor"] = "-1";
                return RedirectToAction("Index");
            }
        }

        //Edit
        public ActionResult Edit(int id)
        {
            Doctors doctor = GetDoctorsList().Where(i => i.Doctor_Id == id).First();
            return View(doctor);
        }

        //Edit Post
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Doctor_Id,Doctor_Name,DeptId")] Doctors doctors)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var putTask = client.PutAsJsonAsync("Doctors/" + doctors.Doctor_Id, doctors);
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Doctor"] = "2";
                    return RedirectToAction("Index");
                }
                return View(doctors);
            }
        }

        //Delete

        public ActionResult Delete(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var DeleteTask = client.DeleteAsync("Doctors/" + id);
                var result = DeleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["Delete"] = "3";
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}