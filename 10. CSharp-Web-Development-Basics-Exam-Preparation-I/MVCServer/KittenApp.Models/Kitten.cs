﻿using System.ComponentModel.DataAnnotations;

namespace KittenApp.Models
{
    public class Kitten
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [Range(0, 20)]
        public int Age { get; set; }

        public int BreedId { get; set; }

        public Breed Breed { get; set; }
    }
}
