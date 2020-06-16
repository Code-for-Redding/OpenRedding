namespace OpenRedding.Client
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Configuration;
    using OpenRedding.Domain.Common.Miscellaneous;
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.ViewModels;

    public class OpenReddingApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public OpenReddingApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["apiBaseUrl"];
        }

        public Task<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>> GetEmployeesSalariesAsync(EmployeeSalarySearchRequestDto? searchRequest)
        {
            var searchRequestUrl = $"{_apiBaseUrl}/salaries";

            if (searchRequest != null)
            {
                var year = searchRequest.Year.HasValue ? searchRequest.Year.Value.ToString() : string.Empty;
                var basePayRange = searchRequest.BasePayRange.HasValue ? searchRequest.BasePayRange.Value.ToString() : string.Empty;
                var totalPayRange = searchRequest.TotalPayRange.HasValue ? searchRequest.TotalPayRange.Value.ToString() : string.Empty;
                var queryParameters = new Dictionary<string, string>();

                if (!string.IsNullOrWhiteSpace(searchRequest.Name))
                {
                    queryParameters.Add(nameof(searchRequest.Name), searchRequest.Name);
                }

                if (!string.IsNullOrWhiteSpace(searchRequest.JobTitle))
                {
                    queryParameters.Add(nameof(searchRequest.JobTitle), searchRequest.JobTitle);
                }

                if (!string.IsNullOrWhiteSpace(year))
                {
                    queryParameters.Add(nameof(year), year);
                }

                if (!string.IsNullOrWhiteSpace(searchRequest.Agency))
                {
                    queryParameters.Add(nameof(searchRequest.Agency), searchRequest.Agency);
                }

                if (!string.IsNullOrWhiteSpace(searchRequest.Status))
                {
                    queryParameters.Add(nameof(searchRequest.Status), searchRequest.Status);
                }

                if (!string.IsNullOrWhiteSpace(basePayRange))
                {
                    queryParameters.Add(nameof(basePayRange), basePayRange);
                }

                if (!string.IsNullOrWhiteSpace(totalPayRange))
                {
                    queryParameters.Add(nameof(totalPayRange), totalPayRange);
                }

                if (!string.IsNullOrWhiteSpace(searchRequest.SortField))
                {
                    queryParameters.Add(nameof(searchRequest.SortField), searchRequest.SortField);
                }

                if (!string.IsNullOrWhiteSpace(searchRequest.SortBy))
                {
                    queryParameters.Add(nameof(searchRequest.SortBy), searchRequest.SortBy);
                }

                searchRequestUrl = QueryHelpers.AddQueryString(searchRequestUrl, queryParameters);
            }

            return _httpClient.GetJsonAsync<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>>(searchRequestUrl);
        }

        public Task<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>> GetEmployeesSalariesFromLinkAsync(string link)
        {
            return _httpClient.GetJsonAsync<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>>(link);
        }

        public Task<EmployeeSalaryDetailViewModel> GetEmployeeSalaryDetailFromId(string id)
        {
            return _httpClient.GetJsonAsync<EmployeeSalaryDetailViewModel>($"{_apiBaseUrl}/salaries/{id}");
        }

        public Task<EmployeeSalaryDetailViewModel> GetEmployeeSalaryDetailFromLink(string link)
        {
            return _httpClient.GetJsonAsync<EmployeeSalaryDetailViewModel>(link);
        }

        public Task<OpenReddingLink> GetDownloadCsvLink(EmployeeSalarySearchRequestDto? searchRequest)
        {
            return _httpClient.PostJsonAsync<OpenReddingLink>($"{_apiBaseUrl}/salaries/download", searchRequest ?? new EmployeeSalarySearchRequestDto());
        }
    }
}
