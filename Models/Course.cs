using ProjetEtudiantBackend.Models;
using StudentBackend.Models.DTO;

namespace ProjetEtudiantBackend.Entity
{
    public class Course
    {
        public Guid CourseId { get; set; }

        public string InstructorId { get; set; }

        public string Name { get; set; }

        public List<Assignment> Assignments { get; set; } = new List<Assignment>();

        public int CourseYear { get; set; }

    }
    public static class CourseExtension
    {
        public static void MapUpdateCourse(this Course course, CreateOrUpdateCourse newCourse)
        {
            course.Name = newCourse.Name;
            course.CourseYear = newCourse.CourseYear;
        }
    }
}
