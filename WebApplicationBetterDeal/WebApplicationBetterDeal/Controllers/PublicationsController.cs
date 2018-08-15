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
    [Route("api/Publications")]
    public class PublicationsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        /*public PublicationsController(ApplicationDbContext context)
        {
            _context = context;
        }*/
        public PublicationsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        : base(userManager)
        {
            _context = context;
        }

        // GET: api/Publications
        [HttpGet]
        public IEnumerable<Publication> GetPublication()
        {
            return _context.Publication.Include(u => u.ApplicationUser).Include(u => u.Responses).ToList();
        }

        // GET: api/Publications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPublication([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var publication = await _context.Publication.Include(u => u.ApplicationUser).Include(u => u.Responses).SingleOrDefaultAsync(m => m.Id == id);

            if (publication == null)
            {
                return NotFound();
            }

            return Ok(publication);
        }

        public async Task<Publication> GetPublicationToResponse(int id)
        {
            var publication = await _context.Publication.SingleOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                // lui dire c qui doit faire sil na pas trouver
            }
            return publication;
        }

        // PUT: api/Publications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublication([FromRoute] int id, [FromBody] Publication publication)
        {
            ApplicationUser applicationUserCur = await GetCurrentUserAsync();
            var entityCur = _context.Publication.Find(id);
            if (applicationUserCur.Status == "admin" || entityCur.ApplicationUserId == applicationUserCur.Id) {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != publication.Id)
                {
                    return BadRequest();
                }

                var entity = _context.Publication.Find(id);
                //entity.ApplicationUser = await _context.ApplicationUser.FindAsync(publication.ApplicationUser);
                entity.Title = publication.Title;
                entity.Description = publication.Description;
                entity.Timestamp = publication.Timestamp;

                // _context.Entry(publication).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicationExists(id))
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
                return BadRequest("Pas admin ou propriétaire");
            }
            
        }

        // POST: api/Publications
        [HttpPost]
        public async Task<IActionResult> PostPublication([FromBody] Publication publication)
        {
            ApplicationUser applicationUserCur = await GetCurrentUserAsync();
            if(applicationUserCur.Status == "admin" || applicationUserCur.Status == "seller")
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (publication.ApplicationUserId != null)
                {
                    ApplicationUsersController applicationUsers = new ApplicationUsersController(getUMgr(), _context);
                    var applicationUser = await applicationUsers.GetUserToPulication(publication.ApplicationUserId);
                    publication.ApplicationUser = applicationUser;
                }
                publication.ApplicationUser = applicationUserCur;

                _context.Publication.Add(publication);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPublication", new { id = publication.Id }, publication);
            }
            else
            {
                return BadRequest("Pas admin ou client");
            }
        }

        // DELETE: api/Publications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublication([FromRoute] int id)
        {
            ApplicationUser applicationUser = await GetCurrentUserAsync();
            var publicationUser = await _context.Publication.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser.Status == "admin" || applicationUser.Id == publicationUser.ApplicationUserId)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // var publication = await _context.Publication.SingleOrDefaultAsync(m => m.Id == id);
                var publication = await _context.Publication.Include(u => u.ApplicationUser).Include(u => u.Responses).SingleOrDefaultAsync(m => m.Id == id);
                if (publication == null)
                {
                    return NotFound();
                }

                //supp
                var responses = publication.Responses;
                if (responses != null) {
                    foreach (var response in responses) {
                        _context.Response.Remove(response);
                    }
                }
                //
                _context.Publication.Remove(publication);
                await _context.SaveChangesAsync();

                return Ok(publication);
            }
            else
            {
                return BadRequest("Pas admin ou pas auteur");
            }

        }

        private bool PublicationExists(int id)
        {
            return _context.Publication.Any(e => e.Id == id);
        }
    }
}