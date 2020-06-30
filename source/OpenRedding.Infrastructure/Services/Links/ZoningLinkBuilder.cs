namespace OpenRedding.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Primitives;
    using OpenRedding.Core.Configuration;
    using OpenRedding.Core.Infrastructure.Services;
    using OpenRedding.Domain.Common.Miscellaneous;
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Zoning.Dtos;
    using OpenRedding.Shared;

    public class ZoningLinkBuilder : ILinkBuilder<ZoningSearchResultDto>
    {
        private readonly string _apiBaseUrl;

        public ZoningLinkBuilder(IOptions<ApplicationSettings> settings)
        {
            ArgumentValidation.CheckNotNull(settings, nameof(settings));

            _apiBaseUrl = string.IsNullOrWhiteSpace(settings.Value.ApiBaseUrl) ?
                throw new ArgumentNullException(nameof(settings)) :
                settings.Value.ApiBaseUrl;
        }

        public OpenReddingLink BuildLink(IEnumerable<KeyValuePair<string, StringValues>> queryCollection, string context, string httpMethod, KeyValuePair<string, string>? trailingParam = null)
        {
            var queryBuilder = new QueryBuilder();

            foreach (var queryParam in queryCollection)
            {
                if (trailingParam is null || !string.Equals(queryParam.Key, trailingParam.Value.Key, StringComparison.CurrentCultureIgnoreCase))
                {
                    queryBuilder.Add(queryParam.Key, queryParam.Value.ToString());
                }
            }

            if (!(trailingParam is null))
            {
                queryBuilder.Add(trailingParam.Value.Key, trailingParam.Value.Value);
            }

            var link = $"{_apiBaseUrl}{context}{queryBuilder.ToQueryString()}";

            return new OpenReddingLink
            {
                Href = link,
                Rel = nameof(OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>),
                Method = httpMethod
            };
        }

        public OpenReddingPagedLinks BuildPaginationLinks(IEnumerable<KeyValuePair<string, StringValues>> queryCollection, int totalPages, int? currentPage)
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

            return new OpenReddingPagedLinks
            {
                Next = BuildLink(queryCollection, "/salaries", HttpMethod.Get.Method, new KeyValuePair<string, string>("page", nextPage.ToString())),
                Previous = BuildLink(queryCollection, "/salaries", HttpMethod.Get.Method, new KeyValuePair<string, string>("page", previousPage.ToString())),
                First = BuildLink(queryCollection, "/salaries", HttpMethod.Get.Method, new KeyValuePair<string, string>("page", "1")),
                Last = BuildLink(queryCollection, "/salaries", HttpMethod.Get.Method, new KeyValuePair<string, string>("page", totalPages.ToString())),
                Paged = BuildLink(queryCollection, "/salaries", HttpMethod.Get.Method, new KeyValuePair<string, string>("page", OpenReddingConstants.PageNumberStringReplacementValue)),
                Download = BuildLink(queryCollection, "/salaries/download", HttpMethod.Get.Method)
            };
        }
    }
}
