using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplicationBetterDeal.Data;
using WebApplicationBetterDeal.Models;

namespace WebApplicationBetterDeal.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        
        private UserManager<ApplicationUser> _userManager;
      
        

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
            
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Person newPers)
        {

            var newUser = new ApplicationUser
            {
                UserName = newPers.UserName,
                Email = newPers.Email,
                Status = newPers.Status,
                AddressShop = newPers.AddressShop,
                NameShop = newPers.NameShop
            };

            

            /*bool x = await _roleManager.RoleExistsAsync("admin");
            if (!x)
            {
                var role = new IdentityRole();
                role.Name = "admin";
                await _roleManager.CreateAsync(role);

            }
            */


            IdentityResult result = await _userManager.CreateAsync(newUser, newPers.Password);
            // TODO: retourner un Created à la place du Ok;

            
            

            return (result.Succeeded) ? Ok() : (IActionResult)BadRequest();
        }
    }
}
