namespace OpenRedding.Identity.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserAccountViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Confirm Email Address")]
        [Compare("Email", ErrorMessage = "The email and confirmation email do not match.")]
        public string? ConfirmEmail { get; set; }

        [Required]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d!$%@#£€*?&]{8,}$", ErrorMessage = "That password does not meet the requirements.")]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d!$%@#£€*?&]{8,}$", ErrorMessage = "That password does not meet the requirements.")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Reason for use is required.")]
        [StringLength(128, ErrorMessage = "The reason for use must be at less than 128 characters long.")]
        [Display(Name = "What brings you to Open Redding?")]
        public string? ReasonForUse { get; set; }
    }
}
