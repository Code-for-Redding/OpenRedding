namespace OpenRedding.Api.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Primitives;
    using OpenRedding.Core.Infrastructure.Services;
    using OpenRedding.Domain.Common.Miscellaneous;
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Shared;

    public class SalariesLinkBuilder : ILinkBuilder
    {
        private readonly string _gatewayBaseUrl;

        public SalariesLinkBuilder(IConfiguration configuration) =>
            _gatewayBaseUrl = configuration["GatewayUrl"];

        public OpenReddingLink BuildLink()
        {
            throw new NotImplementedException();
        }

        public OpenReddingPagedLinks BuildPaginationLinks<TResponse>(IEnumerable<KeyValuePair<string, StringValues>> queryCollection, int totalPages, int? currentPage)
        {
            int nextPage;
            int previousPage;
            if (currentPage.HasValue)
            {
                nextPage = currentPage.Value < totalPages ? currentPage.Value + 1 : 1;
                previousPage = currentPage.Value > 1 && currentPage.Value <= totalPages ? currentPage.Value - 1 : totalPages;
            }
            else
            {
                nextPage = totalPages > 1 ? 2 : 1;
                previousPage = totalPages;
            }

            var nextPageQuery = new QueryBuilder();
            var previousPageQuery = new QueryBuilder();
            var firstPageQuery = new QueryBuilder();
            var lastPageQuery = new QueryBuilder();
            var pagedLinkQuery = new QueryBuilder();
            var downloadLinkQuery = new QueryBuilder();

            foreach (var queryParam in queryCollection)
            {
                if (!string.Equals(queryParam.Key, "page", StringComparison.CurrentCultureIgnoreCase))
                {
                    nextPageQuery.Add(queryParam.Key, queryParam.Value.ToString());
                    previousPageQuery.Add(queryParam.Key, queryParam.Value.ToString());
                    firstPageQuery.Add(queryParam.Key, queryParam.Value.ToString());
                    lastPageQuery.Add(queryParam.Key, queryParam.Value.ToString());
                    pagedLinkQuery.Add(queryParam.Key, queryParam.Value.ToString());
                    downloadLinkQuery.Add(queryParam.Key, queryParam.Value.ToString());
                }
            }

            nextPageQuery.Add("page", nextPage.ToString());
            previousPageQuery.Add("page", previousPage.ToString());
            firstPageQuery.Add("page", "1");
            lastPageQuery.Add("page", totalPages.ToString());
            pagedLinkQuery.Add("page", OpenReddingConstants.PageNumberStringReplacementValue);

            var nextPageLink = $"{_gatewayBaseUrl}/salaries{nextPageQuery.ToQueryString()}";
            var previousPageLink = $"{_gatewayBaseUrl}/salaries{previousPageQuery.ToQueryString()}";
            var firstPageLink = $"{_gatewayBaseUrl}/salaries{firstPageQuery.ToQueryString()}";
            var lastPageLink = $"{_gatewayBaseUrl}/salaries{lastPageQuery.ToQueryString()}";
            var pagedPageLink = $"{_gatewayBaseUrl}/salaries{pagedLinkQuery.ToQueryString()}";
            var downloadRequestLink = $"{_gatewayBaseUrl}/salaries/download{downloadLinkQuery.ToQueryString()}";

            return new OpenReddingPagedLinks
            {
                Next = new OpenReddingLink
                {
                    Href = nextPageLink,
                    Rel = nameof(OpenReddingPagedViewModel<TResponse>),
                    Method = HttpMethod.Get.Method
                },
                Previous = new OpenReddingLink
                {
                    Href = previousPageLink,
                    Rel = nameof(OpenReddingPagedViewModel<TResponse>),
                    Method = HttpMethod.Get.Method
                },
                First = new OpenReddingLink
                {
                    Href = firstPageLink,
                    Rel = nameof(OpenReddingPagedViewModel<TResponse>),
                    Method = HttpMethod.Get.Method
                },
                Last = new OpenReddingLink
                {
                    Href = lastPageLink,
                    Rel = nameof(OpenReddingPagedViewModel<TResponse>),
                    Method = HttpMethod.Get.Method
                },
                Paged = new OpenReddingLink
                {
                    Href = pagedPageLink,
                    Rel = nameof(OpenReddingPagedViewModel<TResponse>),
                    Method = HttpMethod.Get.Method
                },
                Download = new OpenReddingLink
                {
                    Href = downloadRequestLink,
                    Method = HttpMethod.Get.Method
                }
            };
        }
    }
}
