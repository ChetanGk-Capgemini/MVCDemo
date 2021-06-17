using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Demo.Models
{
    public class Outpatients
    {
        public int Pid { get; set; }
        public DateTime Treatment_Date { get; set; }
        public int Doctor_Id { get; set; }
    }
}