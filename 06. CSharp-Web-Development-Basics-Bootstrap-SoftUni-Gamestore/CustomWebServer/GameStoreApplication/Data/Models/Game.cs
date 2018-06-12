namespace CustomWebServer.GameStoreApplication.Data.Models
{
    using Common.ValidationConstants;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Game
    {
        public int Id { get; set; }

        [Required]
        [MinLength(GameConstants.TitleMinLength, ErrorMessage = ValidationConstantsMessages.InvalidMinLengthErrorMessage)]
        [MaxLength(GameConstants.TitleMaxLength, ErrorMessage = ValidationConstantsMessages.InvalidMaxLengthErrorMessage)]
        public string Title { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double Size { get; set; }

        [Required]
        [MinLength(GameConstants.TrailerLength, ErrorMessage = ValidationConstantsMessages.ExactLengthErrorMessage)]
        [MaxLength(GameConstants.TrailerLength, ErrorMessage = ValidationConstantsMessages.ExactLengthErrorMessage)]
        public string Trailer { get; set; }

        public string ThumbnailUrl { get; set; }

        [Required]
        [MinLength(GameConstants.DescriptionMinLength, ErrorMessage = ValidationConstantsMessages.InvalidMinLengthErrorMessage)]
        public string Description { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public ICollection<UserGame> Users { get; set; } = new List<UserGame>();
    }
}
