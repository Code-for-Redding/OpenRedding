namespace OpenRedding.Client
{
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Configuration;
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Salaries.Dtos;

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
            var searchRequestUrl = new StringBuilder($"{_apiBaseUrl}/salaries?");

            if (searchRequest != null)
            {
                var year = searchRequest.Year.HasValue ? searchRequest.Year.Value.ToString() : string.Empty;

                searchRequestUrl.Append($"name={searchRequest.Name}&")
                    .Append($"jobTitle={searchRequest.JobTitle}&")
                    .Append($"agency={searchRequest.Agency}&")
                    .Append($"status={searchRequest.Status}&")
                    .Append($"year={year}&")
                    .Append($"sortField={searchRequest.SortField}&")
                    .Append($"sortBy={searchRequest.SortBy}");
            }

            return await _httpClient.GetJsonAsync<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>>(searchRequestUrl.ToString());
        }

        public async Task<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>> GetEmployeesSalariesFromLinkAsync(string link)
        {
            return await _httpClient.GetJsonAsync<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>>(link);
        }
    }
}
