using ProjetEtudiantBackend.Entity;
using ProjetEtudiantBackend.Models;

namespace StudentBackend.Models.DTO
{
    public class CreateOrUpdateEnrollment
    {
        public Guid CourseId { get; set; }

        public string StudentId { get; set; }

        public decimal Progress { get; set; }
    }
    public static class CreateEnrollmentExtension
    {
        public static Enrollment MapAddEnrollment(this CreateOrUpdateEnrollment enrollment)
        {
            return new Enrollment
            {
                Id = Guid.NewGuid(),

                CourseId = enrollment.CourseId,

                StudentId = enrollment.StudentId,

                Progress = enrollment.Progress

    };
        }
    }
}
