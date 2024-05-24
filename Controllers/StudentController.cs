
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetEtudiantBackend.Entity;
using StudentBackend.Models.DTO;

namespace StudentBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentProjetContext _dbContext;
        public StudentController(StudentProjetContext dbContext) {
            _dbContext = dbContext;
        }

        [HttpGet]    
        public async Task <IActionResult> GetStudents()
        {
            var students = await _dbContext.People.Where( p => p.Role == Role.Student).ToListAsync();
            return Ok(students);

        }

        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetStudent(string studentId)
        {
            var student = await GetStudentAsync(studentId);
            return Ok(student);

        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] CreateOrUpdatePerson student) {
            try {
                var studentMapped = student.MapAddStudent();
                _dbContext.People.Add(studentMapped);
                _dbContext.SaveChanges();
                return Ok(studentMapped.PersonId);

            }
            catch (Exception ex) { return BadRequest(ex.Message); }
            
        }
        [HttpPut("{studentId}")]
        public async Task <IActionResult> UpdateStudent(string studentId, [FromBody] CreateOrUpdatePerson updateStudent) {
            try
            {
                var student = await GetStudentAsync(studentId);
                student.MapUpdateStudent(updateStudent);
                _dbContext.SaveChanges();
                return Ok();

            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{studentId}")]
        public async Task<IActionResult> RemoveStudent(string studentId)
        {
            
           
            try
            {
                _dbContext.People.Remove(await _dbContext.People.FindAsync(studentId));
                _dbContext.SaveChanges();
                return Ok();

            }
            catch (Exception ex) { return BadRequest(ex.Message); }

        }
        private async Task<Person> GetStudentAsync(string personId) { 
            return await _dbContext.People.FirstOrDefaultAsync(p => p.PersonId == personId && p.Role == Role.Student); }
    }
}
