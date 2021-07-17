using MVC_Demo.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC_Demo.Controllers
{
    public class Bill_DataController : Controller
    {

        string Baseurl = "https://localhost:44354/DataAccess/Bills/";
        public ActionResult Index()
        {

            List<Bills> bills = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var result = client.GetAsync("GetAllBills").Result;
                if (result.IsSuccessStatusCode)
                {
                    bills = result.Content.ReadAsAsync<List<Bills>>().Result;
                }
                else
                {
                    bills = null;
                }
            }
            return View(bills);
        }

        // GET: Bill_Data/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Bill_Data/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bill_Data/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pid,Bill_Date,Doctor_Id,Room_Charge,Operation_Charges,Medicine_Fees,Lab_Fees,Total_Amount")] Bills bill)
        {
            return View(bill);
        }
        // GET: Bill_Data/Edit/5
        public ActionResult Edit(int id)
        {
            //ViewBag.JavaScriptFunction = edit;
            //Bill_Data bd = new Bill_Data();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pid,Bill_Date,Doctor_Id,Room_Charge,Operation_Charges,Medicine_Fees,Lab_Fees,Total_Amount")] Bills bill)
        {
            return View(bill);
        }

        // POST: Bill_Data/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: Bill_Data/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bill_Data/Delete/5
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

        public ActionResult Invoice(int id)
        {
            //ViewData["Bill"] = db.Bill_Data.Where(i => i.Bill_No == id).First();
            return View();
        }


    }
}