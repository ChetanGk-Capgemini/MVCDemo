using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Demo.Models
{
    public class Doctors
    {
        public int Doctor_Id { get; set; }
        public string Doctor_Name { get; set; }
        public int DeptId { get; set; }
        public virtual Departments Department { get; set; }
    }
}