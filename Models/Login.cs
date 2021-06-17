using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Demo.Models
{
    public class Login
    {
        public string User_Name{ get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}