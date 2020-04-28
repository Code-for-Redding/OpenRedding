namespace OpenRedding.Identity.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.AspNetCore.WebUtilities;
    using OpenRedding.Infrastructure.Identity;

    [AllowAnonymous]
    public class ResetPassword : PageModel
    {
        private readonly UserManager<OpenReddingUser> _userManager;

        public ResetPassword(UserManager<OpenReddingUser> userManager)
        {
            _userManager = userManager;
            Input = new InputModel();
        }

        [BindProperty]
        public InputModel Input { get; set; }

#pragma warning disable CA1034 // Nested types should not be visible

        public class InputModel
#pragma warning restore CA1034 // Nested types should not be visible
        {
            [Required]
            [EmailAddress]
            public string? Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string? Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string? ConfirmPassword { get; set; }

            public string? Code { get; set; }
        }

#pragma warning disable SA1201 // Elements should appear in the correct order

        public IActionResult OnGet(string? code = null)
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input?.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input?.Code, Input?.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
    }
}
