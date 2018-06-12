namespace CustomWebServer.GameStoreApplication.ViewModels.Account
{ 
    using Common.ValidationConstants;
    using Helpers;
    using System.ComponentModel.DataAnnotations; 

    public class RegisterViewModel
    { 

        [Required]
        [Display(Name = "E-mail")]
        [MinLength(AccountConstants.EmailMinLength, ErrorMessage = ValidationConstantsMessages.InvalidMinLengthErrorMessage)]
        [MaxLength(AccountConstants.EmailMaxLength, ErrorMessage = ValidationConstantsMessages.InvalidMaxLengthErrorMessage)]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        [MinLength(AccountConstants.FullNameMinLength, ErrorMessage = ValidationConstantsMessages.InvalidMinLengthErrorMessage)]
        [MaxLength(AccountConstants.FullNameMaxLength, ErrorMessage = ValidationConstantsMessages.InvalidMaxLengthErrorMessage)]
        public string FullName { get; set; }

        [Required]
        [MinLength(AccountConstants.PasswordMinLength, ErrorMessage = ValidationConstantsMessages.InvalidMinLengthErrorMessage)]
        [MaxLength(AccountConstants.PasswordMaxLength, ErrorMessage = ValidationConstantsMessages.InvalidMaxLengthErrorMessage)]
        [Password]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password))] // Validate if Equal
        public string ConfirmPassword { get; set; }
    }
}
