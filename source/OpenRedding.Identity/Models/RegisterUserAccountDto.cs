namespace OpenRedding.Identity.Models
{
	using System.ComponentModel.DataAnnotations;

    public class RegisterUserAccountDto
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
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "The {0} must be at less than 128 characters long.")]
        [Display(Name = "What brings you to Open Redding?")]
        public string? ReasonForUse { get; set; }
    }
}
