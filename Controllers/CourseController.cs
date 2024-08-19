using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetEtudiantBackend.Entity;
using StudentBackend.Models;
using StudentBackend.Models.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudentBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly StudentProjetContext _dbContext;
        public CourseController(StudentProjetContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _dbContext.Courses.ToListAsync();
            return Ok(courses);
        }

        
        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetCourse(Guid courseId)
        {
            var course = await GetCourseAsync(courseId);
            return course != null ? Ok(course) : NotFound();
        }

        
        [HttpGet("instructor/{instructorId}")]
        public async Task<IActionResult> GetCourseInstructor(string instructorId)
        {
            var courses = await GetCoursesByInstructorIdAsync(instructorId);
            if (courses == null || courses.Count == 0)
            {
                return NotFound();
            }
            return Ok(courses);
        }


        
        [HttpPost]
        public IActionResult CreateCourse([FromBody] CreateOrUpdateCourse course)
        {
            try
            {
                var courseMapped = course.MapAddCourse();
                _dbContext.Courses.Add(courseMapped);
                _dbContext.SaveChanges();
                if (courseMapped == null)
                {
                    return StatusCode(500);
                }

                return CreatedAtAction(nameof(GetCourse), new { courseId = courseMapped.CourseId }, courseMapped);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        
        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourse(Guid courseId, [FromBody] CreateOrUpdateCourse updateCourse)
        {
            try
            {
                var course = await GetCourseAsync(courseId);
                if (course == null) return NotFound();

                course.MapUpdateCourse(updateCourse);
                _dbContext.SaveChanges();
                return Ok(course);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        
        [HttpDelete("{courseId}")]
        public async Task<IActionResult> RemoveCourse(Guid courseId)
        {
            try
            {
                var course = await _dbContext.Courses.FindAsync(courseId);
                if (course == null) return NotFound();

                _dbContext.Courses.Remove(course);
                _dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

 
        private async Task<Course> GetCourseAsync(Guid courseId)
        {
            return await _dbContext.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);
        }


        private async Task<List<Course>> GetCoursesByInstructorIdAsync(string instructorId)
        {
            return await _dbContext.Courses
                                   .Where(c => c.InstructorId == instructorId)
                                   .ToListAsync();
        }
        

    }
}