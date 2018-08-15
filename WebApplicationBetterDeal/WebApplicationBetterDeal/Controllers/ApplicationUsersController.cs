using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationBetterDeal.Data;
using WebApplicationBetterDeal.Models;

namespace WebApplicationBetterDeal.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/ApplicationUsers")]
    public class ApplicationUsersController : BaseController
    {
        private readonly ApplicationDbContext _context;

        /*public ApplicationUsersController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public ApplicationUsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        : base(userManager)
        {
            _context = context;
        }


        // GET: api/ApplicationUsers
        [HttpGet]
        public IEnumerable<ApplicationUser> GetApplicationUser()
        {
            // return _context.ApplicationUser;
            return _context.ApplicationUser.Include(u => u.Publications).Include(u => u.Responses).ToList();
        }

        // GET: api/ApplicationUsers/noub.bil@hotmail.com
        [Route("/username/")]
        [HttpGet("{username}")]
        public async Task<IActionResult> GetApplicationUser([FromRoute] string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = await _context.ApplicationUser.Include(u => u.Publications).SingleOrDefaultAsync(m => m.UserName == userName);
            

            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }

        /*[Route("/id/publications")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublicationFromSeller([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = await _context.ApplicationUser.Include(u => u.Publications).SingleOrDefaultAsync(m => m.Id == id);


            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser.Publications);
        }*/


        public async Task<ApplicationUser> GetUserToPulication(String id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                // lui dire c qui doit faire sil na pas trouver
            }
            return applicationUser;
        }

        // PUT: api/ApplicationUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUser([FromRoute] string id, [FromBody] ApplicationUser applicationUser)
        {
            ApplicationUser applicationUserCurr = await GetCurrentUserAsync();
            if(applicationUserCurr.Status == "admin" || applicationUser.Id == id)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != applicationUser.Id)
                {
                    return BadRequest();
                }

                _context.Entry(applicationUser).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            else
            {
                return BadRequest("pas admin ou utilisateur");
            }
            
        }

        // POST: api/ApplicationUsers
       /* [HttpPost]
        public async Task<IActionResult> PostApplicationUser([FromBody] ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ApplicationUser.Add(applicationUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationUser", new { id = applicationUser.Id }, applicationUser);
        }*/

        // DELETE: api/ApplicationUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationUser([FromRoute] string id)
        {
            ApplicationUser applicationUserCurr = await GetCurrentUserAsync();
            if(applicationUserCurr.Status == "admin")
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
                var applicationUser = await _context.ApplicationUser.Include(u => u.Publications).Include(u => u.Responses).SingleOrDefaultAsync(m => m.Id == id);

                if (applicationUser == null)
                {
                    return NotFound();
                }

                var publications = applicationUser.Publications;

                if (publications != null) {
                    foreach(var publication in publications)
                    {
                        var responsePublication = await _context.Publication.Include(u => u.Responses).SingleOrDefaultAsync(m => m.Id == publication.Id);
                        if (responsePublication.Responses != null) {
                            foreach (var res in responsePublication.Responses) {
                                _context.Response.Remove(res);
                            }
                        }
                        _context.Publication.Remove(publication);
                    }
                }

                /*var responses = applicationUser.Responses;
                if (responses != null)
                {
                    foreach (var response in responses)
                    {
                        _context.Response.Remove(response);
                    }
                }*/

                _context.ApplicationUser.Remove(applicationUser);
                await _context.SaveChangesAsync();

                return Ok(applicationUser);
            }
            else
            {
                return BadRequest("pas admin");
            }
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}