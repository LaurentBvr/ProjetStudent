using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetEtudiantBackend.Entity;
using ProjetEtudiantBackend.Models;
using StudentBackend.Models.DTO;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudentBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileAssignmentController : ControllerBase
    {
        private readonly StudentProjetContext _dbContext;
        public FileAssignmentController(StudentProjetContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetFileAssignments()
        {
            var fileassignments = await _dbContext.FileAssignments.ToListAsync();
            return Ok(fileassignments);
        }

        [HttpGet("{fileassignmentId}")]
        public async Task<IActionResult> GetFileAssignment(Guid fileassignmentId)
        {
            var fileassignment = await GetFileAssignmentAsync(fileassignmentId);
            return fileassignment != null ? Ok(fileassignment) : NotFound();
        }

        [HttpPost]
        public IActionResult CreateFileAssignment([FromBody] CreateOrUpdateFileAssignment fileassignment)
        {
            try
            {
                var fileassignmentMapped = fileassignment.MapAddFileAssignment();
                _dbContext.FileAssignments.Add(fileassignmentMapped);
                _dbContext.SaveChanges();
                if (fileassignmentMapped == null)
                {
                    return StatusCode(500);
                }

                return CreatedAtAction(nameof(GetFileAssignment), new { fileassignmentId = fileassignmentMapped.FileAssignmentId }, fileassignmentMapped);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{fileassignmentId}")]
        public async Task<IActionResult> UpdateFileAssignment(Guid fileassignmentId, [FromBody] CreateOrUpdateFileAssignment updateFileAssignment)
        {
            try
            {
                var fileassignment = await GetFileAssignmentAsync(fileassignmentId);
                if (fileassignment == null) return NotFound();

                fileassignment.MapUpdateFileAssignment(updateFileAssignment);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{fileassignmentId}")]
        public async Task<IActionResult> RemoveFileAssignment(Guid fileassignmentId)
        {
            try
            {
                var fileassignment = await _dbContext.FileAssignments.FindAsync(fileassignmentId);
                if (fileassignment == null) return NotFound();

                _dbContext.FileAssignments.Remove(fileassignment);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<FileAssignment> GetFileAssignmentAsync(Guid fileassignmentId)
        {
            return await _dbContext.FileAssignments.FirstOrDefaultAsync(a => a.FileAssignmentId == fileassignmentId);
        }
    }
}