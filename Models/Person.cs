using StudentBackend.Models.DTO;

namespace ProjetEtudiantBackend.Entity
{
    public class Person
    {
        public string PersonId { get; set; }  

        public string Email { get; set; }   

        public string FirstName { get; set; } 
        public string LastName { get; set; } 

        public string Password { get; set; }

        public Role Role { get; set; }


    }

    public static class PersonExtension
    {
        public static void MapUpdateStudent(this Person student, CreateOrUpdatePerson newStudent)
        {


            student.Email = newStudent.Email;
            student.FirstName = newStudent.FirstName;
            student.LastName = newStudent.LastName;
            student.Password = BCrypt.Net.BCrypt.HashPassword(student.Password);


        }
        public static void MapUpdateInstructor(this Person instructor, CreateOrUpdatePerson newInstructor)
        {


            instructor.Email = newInstructor.Email;
            instructor.FirstName = newInstructor.FirstName;
            instructor.LastName = newInstructor.LastName;
            instructor.Password = BCrypt.Net.BCrypt.HashPassword(instructor.Password);

        }
        public static void MapUpdateAdmin(this Person admin, CreateOrUpdatePerson newAdmin)
        {


            admin.Email = newAdmin.Email;
            admin.FirstName = newAdmin.FirstName;
            admin.LastName = newAdmin.LastName;
            admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);

        }
    }
   
}
