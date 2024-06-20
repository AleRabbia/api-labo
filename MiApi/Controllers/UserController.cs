using Microsoft.AspNetCore.Mvc;
using MiApi.Context;
using MiApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CrearUser([FromBody] User nuevoUser)
        {
            _context.Users.Add(nuevoUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = nuevoUser.Id }, nuevoUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUser(int id, [FromBody] User userActualizado)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.username = userActualizado.username;
            user.password = userActualizado.password;
            user.fullName = userActualizado.fullName;
            user.email = userActualizado.email;
            user.phone = userActualizado.phone;
            user.address = userActualizado.address;
            user.role = userActualizado.role;
            user.isActive = userActualizado.isActive;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Users.Any(u => u.Id == id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
