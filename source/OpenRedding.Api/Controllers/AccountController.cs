namespace OpenRedding.Api.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.Extensions.Logging;
	using OpenRedding.Domain.Account.Dtos;

	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : OpenReddingBaseController
    {
		private readonly ILogger<AccountController> _logger;

		public AccountController(ILogger<AccountController> logger)
		{
			_logger = logger;
		}
    }
}