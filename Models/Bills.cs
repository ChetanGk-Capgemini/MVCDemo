using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Demo.Models
{
    public class Bills
    {
        public int Bill_No { get; set; }
        public int Pid { get; set; }
        public DateTime Bill_Date { get; set; }
        public string Patient_Type { get; set; }
        public int Doctor_Id { get; set; }
        public double Doctor_Fees { get; set; }
        public double Room_Charge { get; set; }
        public double Operation_Charges { get; set; }
        public double Medicine_Fees { get; set; }
        public double Total_Days { get; set; }
        public double Lab_Fees { get; set; }
        public double Total_Amount { get; set; }
        public string Bill_Status { get; set; }
    }
}