using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Demo.Models
{
    public class Inpatients
    {
        public int Pid { get; set; }
        public int Room_No { get; set; }
        public int Doctor_Id { get; set; }
        public DateTime Admission_Date { get; set; }
        public DateTime Discharge_Date { get; set; }
        public double Amount_Per_Day { get; set; }


    }
}
