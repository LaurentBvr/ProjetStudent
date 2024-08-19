using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetEtudiantBackend.Entity;
using ProjetEtudiantBackend.Models;
using StudentBackend.Models;
using StudentBackend.Models.DTO;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudentBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscriptionController : ControllerBase
    {
        private readonly StudentProjetContext _dbContext;
        public InscriptionController(StudentProjetContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetInscriptions()
        {
            var inscriptions = await _dbContext.Inscriptions.ToListAsync();
            return Ok(inscriptions);
        }

        [HttpGet("{inscriptionId}")]
        public async Task<IActionResult> GetInscription(Guid inscriptionId)
        {
            var inscription = await GetInscriptionAsync(inscriptionId);
            return inscription != null ? Ok(inscription) : NotFound();
        }

        [HttpGet("byCourseId/{courseId}")]
        public async Task<IActionResult> GetInscriptionCourse(Guid courseId)
        {
            var inscriptions = await GetInscriptionByCourseIdAsync(courseId);
            
            return Ok(inscriptions);
        }

        [HttpGet("byPersonId/{personId}")]
        public async Task<IActionResult> GetInscriptionPerson(string personId)
        {
            var inscriptions = await GetInscriptionByPersonIdAsync(personId);

            return Ok(inscriptions);
        }

        [HttpPost]
        public IActionResult CreateInscription([FromBody] CreateOrUpdateInscription inscription)
        {
            try
            {
                var inscriptionMapped = inscription.MapAddInscription();
                _dbContext.Inscriptions.Add(inscriptionMapped);
                _dbContext.SaveChanges();
                if (inscriptionMapped == null)
                {
                    return StatusCode(500);
                }

                return CreatedAtAction(nameof(GetInscription), new { inscriptionId = inscriptionMapped.InscriptionId }, inscriptionMapped);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{inscriptionId}")]
        public async Task<IActionResult> UpdateInscription(Guid inscriptionId, [FromBody] CreateOrUpdateInscription updateInscription)
        {
            try
            {
                var inscription = await GetInscriptionAsync(inscriptionId);
                if (inscription == null) return NotFound();

                inscription.MapUpdateInscription(updateInscription);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{inscriptionId}")]
        public async Task<IActionResult> RemoveInscription(Guid inscriptionId)
        {
            try
            {
                var inscription = await _dbContext.Inscriptions.FindAsync(inscriptionId);
                if (inscription == null) return NotFound();

                _dbContext.Inscriptions.Remove(inscription);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<Inscription> GetInscriptionAsync(Guid inscriptionId)
        {
            return await _dbContext.Inscriptions.FirstOrDefaultAsync(a => a.InscriptionId == inscriptionId);
        }
        private async Task<List<Inscription>> GetInscriptionByCourseIdAsync(Guid courseId)
        {
            return await _dbContext.Inscriptions
                                   .Where(c => c.CourseId == courseId)
                                   .ToListAsync();
        }
        private async Task<List<Inscription>> GetInscriptionByPersonIdAsync(string personId)
        {
            return await _dbContext.Inscriptions
                                   .Where(c => c.PersonId == personId)
                                   .ToListAsync();
        }
       
    }
}
