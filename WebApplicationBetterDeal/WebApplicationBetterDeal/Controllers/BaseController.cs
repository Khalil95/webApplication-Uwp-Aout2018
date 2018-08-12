using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplicationBetterDeal.Data;

namespace WebApplicationBetterDeal.Controllers
{
    public abstract class BaseController : Controller
    {
        private UserManager<ApplicationUser> _uMgr;

        public BaseController(UserManager<ApplicationUser> uMgr)
        {
            _uMgr = uMgr;
            
        }
        protected async Task<ApplicationUser> GetCurrentUserAsync()
        {
            if (this.HttpContext.User == null)
                throw new Exception("L'utilisateur n'est pas identifié");
            Claim userNameClaim = this.HttpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (userNameClaim == null)
                throw new Exception("Le token JWT semble ne pas avoir été interprété correctement");
            return await _uMgr.FindByNameAsync(userNameClaim.Value);
        }

        public UserManager<ApplicationUser> getUMgr() {
            return _uMgr;
        }

    }
}
