using MVC_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC_Demo.Controllers
{
    public class LabController : Controller
    {
        string Baseurl = "https://localhost:44354/api/Lab_Information";

        public IEnumerable<Lab_Information> GetLabList()
        {
            IEnumerable<Lab_Information> labList;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var result = client.GetAsync("Lab_Information").Result;
                if (result.IsSuccessStatusCode)
                {
                    labList = result.Content.ReadAsAsync<IList<Lab_Information>>().Result;
                }
                else
                {
                    labList = Enumerable.Empty<Lab_Information>();



                }
            }
            return labList;
        }


        public ActionResult Index()
        {
            return View(GetLabList());
        }

        // GET: Lab/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Lab/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lab/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Lab_Id,LabTestName,Price")] Lab_Information lab)
        {
            lab.Lab_Id = GetLabList().Max(d => d.Lab_Id) + 1;
            if (ModelState.IsValid)
              using (HttpClient client = new HttpClient())
              {
                  client.BaseAddress = new Uri("https://localhost:44354/api/");
                  var postTask = client.PostAsJsonAsync("Lab_Information", lab);
                  var result = postTask.Result;
                  if (result.IsSuccessStatusCode)
                  {
                      return RedirectToAction("Index");
                  }
              }
          return RedirectToAction("Index");
        }

        // GET: Lab/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Lab/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Lab_Id,LabTestName,Price")] Lab_Information lab_Information)
        {
            
            return View();
        }

        // GET: Lab/Delete/5
        public ActionResult Delete(int id)
        {
            return View(GetLabList().Where(l=>l.Lab_Id==id).First());
        }

        // POST: Lab/Delete/5
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
