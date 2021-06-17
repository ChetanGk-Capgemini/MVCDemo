using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Demo.Models
{
    public class Patients
    {
        public int Patient_Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Weight { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public long Phone_No { get; set; }
        public string Disease { get; set; }
    }
}