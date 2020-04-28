namespace OpenRedding.Identity.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [AllowAnonymous]
    public class ForgotPasswordConfirmation : PageModel
    {
        public string? UserEmail { get; set; }

        public void OnGet(string? userEmail)
        {
            UserEmail = userEmail;
        }
    }
}
