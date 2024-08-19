using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetEtudiantBackend.Entity;
using StudentBackend.Models.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudentBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly StudentProjetContext _dbContext;
        public AdminController(StudentProjetContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = await _dbContext.People.Where(p => p.Role == Role.Admin).ToListAsync();
            return Ok(admins);
        }

        [HttpGet("{adminId}")]
        public async Task<IActionResult> GetAdmin(string adminId)
        {
            var admin = await GetAdminAsync(adminId);
            return admin != null ? Ok(admin) : NotFound();
        }

        [HttpPost]
        public IActionResult CreateAdmin([FromBody] CreateOrUpdatePerson admin)
        {
            try
            {
                var adminMapped = admin.MapAddAdmin();
                _dbContext.People.Add(adminMapped);
                _dbContext.SaveChanges();
                if (adminMapped == null)
                {
                    return StatusCode(500);
                }

                return CreatedAtAction(nameof(GetAdmin), new { adminId = adminMapped.PersonId }, adminMapped);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{adminId}")]
        public async Task<IActionResult> UpdateAdmin(string adminId, [FromBody] CreateOrUpdatePerson updateAdmin)
        {
            try
            {
                var admin = await GetAdminAsync(adminId);
                if (admin == null) return NotFound();

                admin.MapUpdateAdmin(updateAdmin);
                _dbContext.SaveChanges();
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{adminId}")]
        public async Task<IActionResult> RemoveAdmin(string adminId)
        {
            try
            {
                var admin = await _dbContext.People.FindAsync(adminId);
                if (admin == null) return NotFound();

                _dbContext.People.Remove(admin);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<Person> GetAdminAsync(string personId)
        {
            return await _dbContext.People.FirstOrDefaultAsync(p => p.PersonId == personId && p.Role == Role.Admin);
        }
    }
}