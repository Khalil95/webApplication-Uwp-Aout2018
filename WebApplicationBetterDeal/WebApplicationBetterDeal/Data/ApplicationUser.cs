using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplicationBetterDeal.Models;

namespace WebApplicationBetterDeal.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        public string Status { get; set; }
        public string NameShop { get; set; }
        public string AddressShop { get; set; }

        public virtual ICollection<Publication> Publications { get; set; }

        public virtual ICollection<Response> Responses { get; set; }

    }
}
