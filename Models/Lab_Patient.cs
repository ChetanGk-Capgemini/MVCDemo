using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Demo.Models
{
    public class Lab_Patient
    {
        public int Lab_Id { set; get; }
        public int PId { set; get; }
        public int Doctor_Id { set; get; }
        public DateTime Test_Date { set; get; }
        public double Price { set; get; }
        public string Billing_Status { set; get; }
        public int Test_Id { set; get; }
    }
}