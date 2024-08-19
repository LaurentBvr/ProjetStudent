using ProjetEtudiantBackend.Entity;
using ProjetEtudiantBackend.Models;

namespace StudentBackend.Models.DTO
{
    public class CreateOrUpdateAssignment
    {
        public string Title { get; set; }

        public Guid CourseId { get; set; }

        public int TotalGrade { get; set; }
    }
    public static class CreateAssignmentExtension
    {
        public static Assignment MapAddAssignment(this CreateOrUpdateAssignment assignment)
        {
            return new Assignment
            {
                AssignmentId = Guid.NewGuid(),

                CourseId = assignment.CourseId,

                Title = assignment.Title,

                TotalGrade = assignment.TotalGrade

    };
        }
    }
}
