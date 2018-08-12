using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    [Route("api/InfoTests")]
    public class InfoTestsController : BaseController
    {
        private readonly ApplicationDbContext _context;

       
        /*public InfoTestsController(ApplicationDbContext context)
        {
            _context = context;
        }*/

        public InfoTestsController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
            : base(userManager)
        {
            _context = context;
        }

        // GET: api/InfoTests
        [HttpGet]
        public async Task<IEnumerable<InfoTest>> GetInfoTestAsync()
        {
            ApplicationUser applicationUser = await GetCurrentUserAsync();
            return _context.InfoTest;
        }

        // GET: api/InfoTests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfoTest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var infoTest = await _context.InfoTest.SingleOrDefaultAsync(m => m.Id == id);

            if (infoTest == null)
            {
                return NotFound();
            }

            return Ok(infoTest);
        }

        // PUT: api/InfoTests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfoTest([FromRoute] int id, [FromBody] InfoTest infoTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infoTest.Id)
            {
                return BadRequest();
            }

            // _context.Entry(infoTest).State = EntityState.Modified;
            var entity = _context.InfoTest.Find(id);
            entity.Information = infoTest.Information;
            entity.Timestamp = infoTest.Timestamp;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfoTestExists(id))
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

        // POST: api/InfoTests
        [HttpPost]
        public async Task<IActionResult> PostInfoTest([FromBody] InfoTest infoTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.InfoTest.Add(infoTest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInfoTest", new { id = infoTest.Id }, infoTest);
        }

        // DELETE: api/InfoTests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfoTest([FromRoute] int id)
        {
            ApplicationUser applicationUser = await GetCurrentUserAsync();
            if (applicationUser.Status == "admin")
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var infoTest = await _context.InfoTest.SingleOrDefaultAsync(m => m.Id == id);
                if (infoTest == null)
                {
                    return NotFound();
                }

                _context.InfoTest.Remove(infoTest);
                await _context.SaveChangesAsync();

                return Ok(infoTest);
            }
            else {
                return BadRequest("Pas admin");
            }
        }

        private bool InfoTestExists(int id)
        {
            return _context.InfoTest.Any(e => e.Id == id);
        }
    }
}