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
    [Route("api/Responses")]
    public class ResponsesController : BaseController
    {
        private readonly ApplicationDbContext _context;


        /*public ResponsesController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public ResponsesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
            : base(userManager)
        {
            _context = context;
        }

        // GET: api/Responses
        [HttpGet]
        public IEnumerable<Response> GetResponse()
        {
            return _context.Response.Include(r => r.ApplicationUser).Include(r => r.Publication).ToList();
        }

        // GET: api/Responses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetResponse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _context.Response.Include(r => r.ApplicationUser).Include(r => r.Publication).SingleOrDefaultAsync(m => m.Id == id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // PUT: api/Responses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResponse([FromRoute] int id, [FromBody] Response response)
        {
            ApplicationUser applicationUser = await GetCurrentUserAsync();
            if(applicationUser.Status == "admin" || response.ApplicationUserId == applicationUser.Id)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != response.Id)
            {
                return BadRequest();
            }

            _context.Entry(response).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResponseExists(id))
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

        // POST: api/Responses
        [HttpPost]
        public async Task<IActionResult> PostResponse([FromBody] Response response)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (response.ApplicationUserId != null && response.PublicationId != 0)
            {
                ApplicationUsersController applicationUsers = new ApplicationUsersController(getUMgr(),_context);
                PublicationsController publicationsController = new PublicationsController(getUMgr(), _context);
                var applicationUser = await applicationUsers.GetUserToPulication(response.ApplicationUserId);
                var publication = await publicationsController.GetPublicationToResponse(response.PublicationId);
                response.ApplicationUser = applicationUser;
                response.Publication = publication;
            }

            _context.Response.Add(response);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResponse", new { id = response.Id }, response);
        }

        // DELETE: api/Responses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResponse([FromRoute] int id)
        {
            ApplicationUser applicationUser = await GetCurrentUserAsync();
            var responseCurr = await _context.Response.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser.Status == "admin" || responseCurr.ApplicationUserId == applicationUser.Id)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await _context.Response.SingleOrDefaultAsync(m => m.Id == id);
                if (response == null)
                {
                    return NotFound();
                }

                _context.Response.Remove(response);
                await _context.SaveChangesAsync();

                return Ok(response);
            }
            else {
                return BadRequest("pas admin");
            }
        }

        private bool ResponseExists(int id)
        {
            return _context.Response.Any(e => e.Id == id);
        }
    }
}