using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Models.ViewModels
{
    public class AddStudentRequest
    {
        [Column(TypeName = "nvarchar(50)")]
        public string FullName { get; set; }

        [Column(TypeName = "nvarchar(6)")]
        public Gender Gender { get; set; }

        [Column(TypeName = "nvarchar(12)")]
        public string PhoneNo { get; set; }

        [Column(TypeName = "char")]
        public char Section { get; set; }

        [Column(TypeName = "nvarchar(2)")]
        public string Degree { get; set; }

        [Column(TypeName = "nvarchar(10)")]
        public string DegreeProgram { get; set; }
    }
}
