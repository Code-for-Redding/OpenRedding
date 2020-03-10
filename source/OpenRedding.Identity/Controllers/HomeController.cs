namespace OpenRedding.Identity.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using IdentityServer4.Services;
    using Microsoft.AspNetCore.Mvc;
    using OpenRedding.Domain.Common.Dto;
    using OpenRedding.Domain.Common.ViewModels;

    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interactionService;

        public HomeController(IIdentityServerInteractionService interactionService)
        {
            _interactionService = interactionService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Error(string errorId)
        {
            var errorList = new List<OpenReddingErrorDto>();

            // retrieve error details from identityserver
            var message = await _interactionService.GetErrorContextAsync(errorId);
            if (message != null)
            {
                errorList.Add(new OpenReddingErrorDto(message.Error, message.ErrorDescription));
            }

            return View("Error", new OpenReddingErrorViewModel("An unexpected error has occured", errorList));
        }
    }
}