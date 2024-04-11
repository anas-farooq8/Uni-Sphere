﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uni_Sphere.Models.Domain
{
    public class Departments
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(10)]
        public string Code { get; set; }

        [MaxLength(350)]
        public string? Description { get; set; }


        // Many to One Relationship with Students and Teachers
        public ICollection<Students> Students { get; set; }
        public ICollection<Teachers> Teachers { get; set; }


    }
}