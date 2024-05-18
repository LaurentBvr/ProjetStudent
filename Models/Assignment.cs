

using ProjetEtudiantBackend.Entity;
using StudentBackend.Models.DTO;

namespace ProjetEtudiantBackend.Models
{
    public class Assignment
    {
        public Guid AssignmentId { get; set; }

        public Guid CourseId { get; set; }

        public string Title { get; set; }

        public int TotalGrade { get; set; }


    }
    public static class AssignmentExtension
    {
        public static void MapUpdateAssignment(this Assignment assignment, CreateOrUpdateAssignment newAssignment)
        {
            assignment.Title = newAssignment.Title;
            assignment.CourseId = newAssignment.CourseId;
            assignment.TotalGrade = newAssignment.TotalGrade;
        }
    }
}
