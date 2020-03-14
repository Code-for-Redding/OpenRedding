namespace OpenRedding.Identity.Pages
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.RazorPages;

#pragma warning disable SA1649 // File name should match first type name

    public class FaqModel : PageModel
#pragma warning restore SA1649 // File name should match first type name
    {
        public async Task OnGetAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
        }
    }
}
