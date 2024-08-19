
using ProjetEtudiantBackend.Models;
using StudentBackend.Models.DTO;
using System.Reflection;

namespace ProjetEtudiantBackend.Models
    {
        public class FileAssignment
        {
            public Guid FileAssignmentId { get; set; }

            public Guid StudentId { get; set; }

            public Guid AssingmentId { get; set; }

            public string Title { get; set; }

            public decimal Grade { get; set; }


        }
        public static class FileAssignmentExtension
        {
            public static void MapUpdateFileAssignment(this FileAssignment fileassignment, CreateOrUpdateFileAssignment newFileAssignment)
            {
                fileassignment.StudentId = newFileAssignment.StudentId;
                fileassignment.AssingmentId = newFileAssignment.AssingmentId;
                fileassignment.Title = newFileAssignment.Title;
                fileassignment.Grade = newFileAssignment.Grade;
            }
        }
    }

