using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using MVC_Demo.Models;
using Newtonsoft.Json;

namespace MVC_Demo.Controllers
{
    

    public class RoomController : Controller
    {
        string Baseurl = "https://localhost:44354/DataAccess/RoomData/";
        // GET: Room/Details
        public ActionResult Index()
        {
            IEnumerable<Room> rooms = null;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var result = client.GetAsync("GetAllRooms").Result;
                if (result.IsSuccessStatusCode)
                {
                    rooms = result.Content.ReadAsAsync<IList<Room>>().Result;
                }
                else
                {
                    rooms = Enumerable.Empty<Room>();
                }
            }
            return View(rooms);
        }

        // GET: Room/Details/5
        public ActionResult Details(int id)
        {
            Room room = new Room();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var result = client.GetAsync("Rooms/"+id).Result;
                
                if (result.IsSuccessStatusCode)
                {
                    string stringData = result.Content.ReadAsStringAsync().Result;

                    room = JsonConvert.DeserializeObject<Room>(stringData);
                    return View(room);
                }
                else
                {
                    return View(room);
                }
            }
            
        }

        // GET: Room/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Room/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Room_No,Room_Specification,Price")] Room room)
        {
            try
            {
                // TODO: Add insert logic here
                
                var client = new HttpClient();
                //client.PostAsJsonAsync<Room>(Baseurl+"Rooms");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Room/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Room/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Room_No,Room_Specification,Price")] Room room)
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

        // GET: Room/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Room/Delete/5
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
