namespace OpenRedding.Infrastructure.Services
{
	using System;
	using System.Collections;
    using System.Collections.Generic;
	using Microsoft.Extensions.Configuration;
	using OpenRedding.Core.Infrastructure.Services;
    using OpenRedding.Domain.Common.Miscellaneous;
    using OpenRedding.Domain.Common.ViewModels;

	public class LinkBuilder<TResult> : ILinkBuilder<TResult>
	{
		private readonly string _baseUrl;
		private readonly string _apiVersion;

		public LinkBuilder(IConfiguration configuration)
		{
			_baseUrl = configuration["ApiBaseUrl"];
			_apiVersion = configuration["ApiVersion"];
		}

		public OpenReddingPagedLinks GenerateLinks(IEnumerable<TResult> results)
		{
			throw new NotImplementedException();
		}
	}
}
