namespace OpenRedding.Client
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.WebUtilities;
    using Microsoft.Extensions.Configuration;
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

        public async Task<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>> GetEmployeesSalariesAsync(EmployeeSalarySearchRequestDto? searchRequest)
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
                    queryParameters.Add("name", searchRequest.Name);
                }

                if (!string.IsNullOrWhiteSpace(searchRequest.JobTitle))
                {
                    queryParameters.Add("jobTitle", searchRequest.JobTitle);
                }

                if (!string.IsNullOrWhiteSpace(year))
                {
                    queryParameters.Add("year", year);
                }

                if (!string.IsNullOrWhiteSpace(searchRequest.Agency))
                {
                    queryParameters.Add("agency", searchRequest.Agency);
                }

                if (!string.IsNullOrWhiteSpace(searchRequest.Status))
                {
                    queryParameters.Add("status", searchRequest.Status);
                }

                if (!string.IsNullOrWhiteSpace(basePayRange))
                {
                    queryParameters.Add("basePayRange", basePayRange);
                }

                if (!string.IsNullOrWhiteSpace(totalPayRange))
                {
                    queryParameters.Add("totalPayRange", totalPayRange);
                }

                if (!string.IsNullOrWhiteSpace(searchRequest.SortField))
                {
                    queryParameters.Add("sortField", searchRequest.SortField);
                }

                if (!string.IsNullOrWhiteSpace(searchRequest.SortBy))
                {
                    queryParameters.Add("sortBy", searchRequest.SortBy);
                }

                searchRequestUrl = QueryHelpers.AddQueryString(searchRequestUrl, queryParameters);
            }

            return await _httpClient.GetJsonAsync<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>>(searchRequestUrl);
        }

        public async Task<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>> GetEmployeesSalariesFromLinkAsync(string link)
        {
            return await _httpClient.GetJsonAsync<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>>(link);
        }

        public async Task<EmployeeSalaryDetailViewModel> GetEmployeeSalaryDetailFromLink(string link)
        {
            return await _httpClient.GetJsonAsync<EmployeeSalaryDetailViewModel>(link);
        }

        public async Task DownloadSalaryReport(EmployeeSalarySearchRequestDto? searchRequest)
        {
            await _httpClient.PostJsonAsync($"{_apiBaseUrl}/salaries/download", searchRequest ?? new EmployeeSalarySearchRequestDto());
        }
    }
}
