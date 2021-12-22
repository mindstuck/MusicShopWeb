using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicShopWebAPI.Data;
using MusicShopWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicShopWebAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MusicShopController : ControllerBase
    {
        private readonly MusicShopContext _context;

        public MusicShopController(MusicShopContext context)
        {
            _context = context;
        }

        [HttpGet("instruments/{id?}")]
        public async Task<IEnumerable<MusicInstrument>> Get(int? id, string search)
        {
            IEnumerable<MusicInstrument> instruments = await _context.Instruments.ToListAsync();

            if (!String.IsNullOrEmpty(search))
            {
                instruments = instruments.Where(i => i.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0
                || i.Material.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            }

            if (id.HasValue) { return instruments.Where(i => i.Id == id.Value); }
            return instruments;
        }

        [HttpPost("instruments")]
        public async Task<IActionResult> Post(MusicInstrument instrument)
        {
            if (ModelState.IsValid) { 
                _context.Instruments.Add(instrument);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("instruments/{id}")]
        public async Task<IActionResult> Put(int id, MusicInstrument instrument)
        {
            if (id == instrument.Id && ModelState.IsValid)
            {
                _context.Entry(instrument).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("instruments/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Instruments.Any(i => i.Id == id))
            {
                _context.Instruments.Remove(_context.Instruments.Find(id));
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("users/{id?}")]
        public async Task<IEnumerable<User>> GetUsers(int? id, string search)
        {
            IEnumerable<User> users = await _context.Users.ToListAsync();

            if (!String.IsNullOrEmpty(search))
            {
                users = users.Where(i => i.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0
                || i.Email.ToString().IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            }

            if (id.HasValue) { return users.Where(i => i.Id == id.Value); }
            return users;
        }

        [HttpPost("users")]
        public async Task<IActionResult> PostUser(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }



        // api/email - checking JWT
        [HttpGet("email")]
        public ActionResult<IEnumerable<string>> NameIdentifier()
        {
            var nameIdentifier = this.HttpContext.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            return new string[] { nameIdentifier?.Value, "value1", "value2" };
        }
    }
}
