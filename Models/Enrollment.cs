namespace ProjetEtudiantBackend.Entity
{
    public class Enrollment
    {
        public Guid Id { get; set; } 

        public Guid CourseId { get; set; }

        public Guid PersonId { get; set; }

        public decimal Progress { get; set; } // Sa progression

    }
}
