using ProjetEtudiantBackend.Models;


namespace StudentBackend.Models.DTO
{
    public class CreateOrUpdateFileAssignment
    {
        public Guid FileAssignmentId { get; set; }

        public Guid StudentId { get; set; }

        public Guid AssingmentId { get; set; }

        public string Title { get; set; }

        public decimal Grade { get; set; }

    }
    public static class CreateFileAssignmentExtension
    {
        public static FileAssignment MapAddFileAssignment(this CreateOrUpdateFileAssignment fileassignment)
        {
            return new FileAssignment
            {
                FileAssignmentId = Guid.NewGuid(),

                StudentId = fileassignment.StudentId,

                AssingmentId = fileassignment.AssingmentId,

                Title = fileassignment.Title,

                Grade = fileassignment.Grade

            };
        }
    }
}

