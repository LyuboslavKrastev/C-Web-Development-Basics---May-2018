namespace CustomWebServer.GameStoreApplication.ViewModels.Admin
{
    using Common.ValidationConstants;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AdminEditGameViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(GameConstants.TitleMinLength, ErrorMessage = ValidationConstantsMessages.InvalidMinLengthErrorMessage)]
        [MaxLength(GameConstants.TitleMaxLength, ErrorMessage = ValidationConstantsMessages.InvalidMaxLengthErrorMessage)]
        public string Title { get; set; }

        [Required]
        [MinLength(GameConstants.DescriptionMinLength, ErrorMessage = ValidationConstantsMessages.InvalidMinLengthErrorMessage)]
        public string Description { get; set; }

        public string Thumbnail { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public double Size { get; set; }

        [Required]
        [Display(Name = "Youtube Video URL")]
        [MinLength(GameConstants.TrailerLength, ErrorMessage = ValidationConstantsMessages.ExactLengthErrorMessage)]
        [MaxLength(GameConstants.TrailerLength, ErrorMessage = ValidationConstantsMessages.ExactLengthErrorMessage)]
        public string Trailer { get; set; }

        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }
    }
}
