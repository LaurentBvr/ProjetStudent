using ProjetEtudiantBackend.Entity;
using ProjetEtudiantBackend.Models;

namespace StudentBackend.Models.DTO
{
    public class CreateOrUpdateCourse
    {
        public string Name { get; set; }
        public int CourseYear { get; set; }

        public string InstructorId { get; set; }
    }

    public static class CreateCourseExtension
    {
        public static Course MapAddCourse(this CreateOrUpdateCourse course)
        {
            return new Course
            {
                CourseId = Guid.NewGuid(),

                InstructorId = course.InstructorId,

                Name = course.Name,

                Assignments = new List<Assignment>(),

                CourseYear = course.CourseYear

            };
        }
    }
}