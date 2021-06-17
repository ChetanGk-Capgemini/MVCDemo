using MVC_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC_Demo.Controllers
{
    public class ReportController : Controller
    {
        string Baseurl = "https://localhost:44354/DataAccess/LabPatient/";
        public ActionResult Index()
        {
            IEnumerable<Lab_Patient> lab_Patient = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var result = client.GetAsync("GetLabTestDetails").Result;
                if (result.IsSuccessStatusCode)
                {
                    lab_Patient = result.Content.ReadAsAsync<IList<Lab_Patient>>().Result;
                }
                else
                {
                    lab_Patient = Enumerable.Empty<Lab_Patient>();



                }
            }
            return View(lab_Patient);
        }

        // GET: Report/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Report/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Report/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PId,Doctor_Id,Test_Date,Price,Billing_Status,Test_Id")] Lab_Patient lab)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Report/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PId,Doctor_Id,Test_Date,Price,Billing_Status,Test_Id")] Lab_Patient lab)
        {
            
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Report/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Report/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
