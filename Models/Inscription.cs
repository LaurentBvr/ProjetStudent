using ProjetEtudiantBackend.Entity;
using ProjetEtudiantBackend.Models;
using StudentBackend.Models.DTO;

namespace StudentBackend.Models
{
    public class Inscription
    {

        public Guid InscriptionId { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        public string PersonId { get; set; }


    }
    public static class InscriptionExtension
    {
        public static void MapUpdateInscription(this Inscription inscription, CreateOrUpdateInscription newInscription)
        {
            inscription.CourseId = newInscription.CourseId;
            inscription.PersonId = newInscription.PersonId;
        }
    }
}
