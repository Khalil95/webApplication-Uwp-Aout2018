using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationBetterDeal.Models
{
    public class LoginInfo
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string NameShop { get; set; }
        public string AddressShop { get; set; }

        public LoginInfo()
        {
        }
    }
}
