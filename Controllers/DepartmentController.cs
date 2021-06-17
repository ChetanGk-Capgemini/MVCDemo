using MVC_Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVC_Demo.Controllers
{
    public class DepartmentController : Controller
    {
        string Baseurl = "https://localhost:44354/DataAccess/Department/";

        public IEnumerable<Departments> GetDepList()
        {
            IEnumerable<Departments> departments = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var result = client.GetAsync("GetAllDep").Result;
                if (result.IsSuccessStatusCode)
                {
                    departments = result.Content.ReadAsAsync<IList<Departments>>().Result;
                }
                else
                {
                    departments = Enumerable.Empty<Departments>();
                }
            }
            return departments;
        }
        public ActionResult Index()
        {
            return View(GetDepList());
        }

        public ActionResult Details(int id)
        {
            Departments departments = GetDepList().Where(i => i.DeptId == id).First();
            return View(departments);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeptId,DepartmentName")] Departments department)
        {
            department.DeptId = GetDepList().Max(d => d.DeptId) + 1;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/api/");
                var postTask = client.PostAsJsonAsync("Department", department);
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["DepEdit"] = "1";
                    return RedirectToAction("Index");
                }
            }
            return View(department);
        }

        public ActionResult Edit(int id)
        {
            IEnumerable<Departments> departments = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var result = client.GetAsync("GetAllDep").Result;
                if (result.IsSuccessStatusCode)
                {
                    departments = result.Content.ReadAsAsync<IList<Departments>>().Result;
                }
                else
                {
                    departments = Enumerable.Empty<Departments>();
                }
            }
            Departments department = departments.Where(d => d.DeptId == id).First(); 
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeptId,DepartmentName")] Departments department)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44354/api/");
                var putTask = client.PutAsJsonAsync("Department/" + department.DeptId, department);
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["DepEdit"] = "2";
                    return RedirectToAction("Index");
                }
                return View(department);
            }
        }


        public ActionResult Delete(int id)
        {

            using (HttpClient client = new HttpClient())
            {
                TempData.Keep();
                client.BaseAddress = new Uri(Baseurl);
                var result = client.GetAsync("DeletetDep/"+id).Result;
                
                if (result.IsSuccessStatusCode)
                {
                    TempData["DepEdit"] = "3";
                }
                else
                    TempData["DepEdit"] = "-3";
            }
            //return View();
            return RedirectToAction("Index");
        }
    }
}