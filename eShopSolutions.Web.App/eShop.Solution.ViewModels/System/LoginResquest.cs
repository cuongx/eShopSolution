using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Solution.ViewModels.System
{
   public  class LoginResquest
    {
   
        public  string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
 
}
