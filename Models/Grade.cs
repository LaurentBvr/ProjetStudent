namespace ProjetEtudiantBackend.Models
{
    public class Grade
    {
        public Guid GradeId { get; set; }

        public Guid StudentId { get; set; }

        public Guid AssingmentId { get; set; }

        public decimal Value { get; set; }

       
    }
}
