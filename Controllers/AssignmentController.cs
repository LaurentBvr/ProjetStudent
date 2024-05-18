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
    public class AssignmentController : ControllerBase
    {
        private readonly StudentProjetContext _dbContext;
        public AssignmentController(StudentProjetContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAssignments()
        {
            var assignments = await _dbContext.Assignments.ToListAsync();
            return Ok(assignments);
        }

        [HttpGet("{assignmentId}")]
        public async Task<IActionResult> GetAssignment(Guid assignmentId)
        {
            var assignment = await GetAssignmentAsync(assignmentId);
            return assignment != null ? Ok(assignment) : NotFound();
        }

        [HttpPost]
        public IActionResult CreateAssignment([FromBody] CreateOrUpdateAssignment assignment)
        {
            try
            {
                var assignmentMapped = assignment.MapAddAssignment();
                _dbContext.Assignments.Add(assignmentMapped);
                _dbContext.SaveChanges();
                return Ok(assignmentMapped.AssignmentId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{assignmentId}")]
        public async Task<IActionResult> UpdateAssignment(Guid assignmentId, [FromBody] CreateOrUpdateAssignment updateAssignment)
        {
            try
            {
                var assignment = await GetAssignmentAsync(assignmentId);
                if (assignment == null) return NotFound();

                assignment.MapUpdateAssignment(updateAssignment);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{assignmentId}")]
        public async Task<IActionResult> RemoveAssignment(Guid assignmentId)
        {
            try
            {
                var assignment = await _dbContext.Assignments.FindAsync(assignmentId);
                if (assignment == null) return NotFound();

                _dbContext.Assignments.Remove(assignment);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<Assignment> GetAssignmentAsync(Guid assignmentId)
        {
            return await _dbContext.Assignments.FirstOrDefaultAsync(a => a.AssignmentId == assignmentId);
        }
    }
}