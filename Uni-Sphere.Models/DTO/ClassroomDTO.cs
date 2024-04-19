using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uni_Sphere.Models.Domain;

namespace Uni_Sphere.Models.DTO
{
    public class ClassroomDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public int Batch { get; set; }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }

        // students
        public ICollection<StudentDTO> Students { get; set; }

        public int NoOfStudents { get; set; }

        // posts
        public ICollection<DiscussionPost> DiscussionPosts { get; set; }

    }
}
