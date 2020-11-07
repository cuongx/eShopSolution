using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Solution.ViewModels.System
{
  public class RegisterRequest
    {
        public string FistName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CorfimPassword { get; set; }
    }
}
