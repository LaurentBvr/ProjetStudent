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
    public class InstructorController : ControllerBase
    {
        private readonly StudentProjetContext _dbContext;
        public InstructorController(StudentProjetContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetInstructors()
        {
            var instructors = await _dbContext.People.Where(p => p.Role == Role.Instructor).ToListAsync();
            return Ok(instructors);
        }

        [HttpGet("{instructorId}")]
        public async Task<IActionResult> GetInstructor(string instructorId)
        {
            var instructor = await GetInstructorAsync(instructorId);
            return instructor != null ? Ok(instructor) : NotFound();
        }

        [HttpPost]
        public IActionResult CreateInstructor([FromBody] CreateOrUpdatePerson instructor)
        {
            try
            {
                var instructorMapped = instructor.MapAddInstructor();
                _dbContext.People.Add(instructorMapped);
                _dbContext.SaveChanges();
                return Ok(instructorMapped.PersonId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{instructorId}")]
        public async Task<IActionResult> UpdateInstructor(string instructorId, [FromBody] CreateOrUpdatePerson updateInstructor)
        {
            try
            {
                var instructor = await GetInstructorAsync(instructorId);
                if (instructor == null) return NotFound();

                instructor.MapUpdateInstructor(updateInstructor);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{instructorId}")]
        public async Task<IActionResult> RemoveInstructor(Guid instructorId)
        {
            try
            {
                var instructor = await _dbContext.People.FindAsync(instructorId);
                if (instructor == null) return NotFound();

                _dbContext.People.Remove(instructor);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<Person> GetInstructorAsync(string personId)
        {
            return await _dbContext.People.FirstOrDefaultAsync(p => p.PersonId == personId && p.Role == Role.Instructor);
        }
    }
}