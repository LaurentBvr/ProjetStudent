using ProjetEtudiantBackend.Entity;

namespace StudentBackend.Models.DTO
{
    public class CreateOrUpdatePerson
    {
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Password { get; set; }

        
    }

    public static class CreatePersonExtension
    {
        public static Person MapAddStudent(this CreateOrUpdatePerson student)
        {
            return new Person
            {
                PersonId = GenerateMatricule(student),
                Email = student.Email,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Password = student.Password,
                Role = Role.Student

            };
        }
        public static Person MapAddInstructor(this CreateOrUpdatePerson instructor)
        {
            return new Person
            {
                PersonId = GenerateMatricule(instructor),
                Email = instructor.Email,
                FirstName = instructor.FirstName,
                LastName = instructor.LastName,
                Password = instructor.Password,
                Role = Role.Instructor
            };
        }
        private static string GenerateMatricule(CreateOrUpdatePerson person)
        {
            return person.FirstName.Substring(0, 2) + person.LastName.Substring(0, 2) + new Random().NextInt64(100, 999);
        }

    }
}