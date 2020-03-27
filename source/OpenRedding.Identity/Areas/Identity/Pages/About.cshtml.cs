namespace OpenRedding.Identity.Areas.Identity.Pages
{
    using System;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using OpenRedding.Shared.Validation;

#pragma warning disable SA1649 // File name should match first type name

    public class AboutModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        public string? ReturnUrl { get; set; }

        public void OnGet(Uri returnUrl)
        {
            Validate.NotNull(returnUrl, nameof(returnUrl));
            ReturnUrl = returnUrl.OriginalString;
        }
    }
}
