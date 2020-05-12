namespace OpenRedding.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
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

            // _apiBaseUrl = configuration["apiBaseUrl"];
            _apiBaseUrl = "https://localhost:5003/api/salaries?jobTitle=engineer";
        }

        public async Task<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>> GetEmployeesSalariesAsync()
        {
            var employees = await _httpClient.GetJsonAsync<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>>(_apiBaseUrl);
            Console.WriteLine(employees.Count);
            Console.WriteLine(employees.Pages);
            Console.WriteLine(employees.Links);
            Console.WriteLine(employees.Links?.Next);
            Console.WriteLine(employees.Links?.Next?.Href);

            return employees;
        }

        public async Task<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>> GetEmployeesSalariesFromLinkAsync(string link)
        {
            var employees = await _httpClient.GetJsonAsync<OpenReddingPagedViewModel<EmployeeSalarySearchResultDto>>(link);

            return employees;
        }
    }
}
