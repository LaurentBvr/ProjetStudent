using ProjetEtudiantBackend.Models;

namespace StudentBackend.Models.DTO
{
        public class CreateOrUpdateInscription
        {
            public Guid CourseId { get; set; }

            public string PersonId { get; set; }

            public string PersonFirstName { get; set; }

            public string PersonLastName { get; set; }


    }
        public static class CreateInscriptionExtension
        {
            public static Inscription MapAddInscription(this CreateOrUpdateInscription inscription)
            {
                return new Inscription
                {
                    InscriptionId = Guid.NewGuid(),

                    CourseId = inscription.CourseId,

                    PersonId = inscription.PersonId

                };
            }
        }
 }