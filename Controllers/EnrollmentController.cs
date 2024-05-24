using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetEtudiantBackend.Entity;
using StudentBackend.Models.DTO;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentController : ControllerBase
{
    private readonly StudentProjetContext _dbContext;
    public EnrollmentController(StudentProjetContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetEnrollments()
    {
        var enrollment = await _dbContext.Enrollments.ToListAsync();
        return Ok(enrollment);
    }
    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetEnrollmentByStudent(string studentId)
    {
        var enrollment = await _dbContext.Enrollments.Where(e => e.StudentId == studentId  ).ToListAsync();
        return Ok(enrollment);
    }
    [HttpPost]
    public IActionResult CreateEnrollment([FromBody] CreateOrUpdateEnrollment enrollment)
    {
        try
        {
            var enrollmentMapped = enrollment.MapAddEnrollment();
            _dbContext.Enrollments.Add(enrollmentMapped);
            _dbContext.SaveChanges();
            return Ok(enrollmentMapped.CourseId);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
