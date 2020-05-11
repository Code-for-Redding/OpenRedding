namespace OpenRedding.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.ViewModels;

    public class OpenReddingApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;
        private readonly EmployeeSearchResultViewModelList _stubSearchResult;

        public OpenReddingApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiBaseUrl = configuration["apiBaseUrl"];

            Console.WriteLine($"_apiBaseUrl: {_apiBaseUrl}");

            // Initialize stub data
            var mockEmployees = new List<EmployeeSalarySearchResultDto>
                {
                    new EmployeeSalarySearchResultDto(1, "John Smith", "Engineer", "Redding", "Full Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "John Smith", "Engineer", "Redding", "Full Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "John Smith", "Engineer", "Redding", "Full Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "John Smith", "Engineer", "Redding", "Full Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "John Smith", "Engineer", "Redding", "Full Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "John Smith", "Engineer", "Redding", "Full Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "John Smith", "Engineer", "Redding", "Full Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m),
                    new EmployeeSalarySearchResultDto(1, "Mary Johnson", "Accountant", "Shasta County", "Part Time", 2019, 100000m, 120000m)
                };
            _stubSearchResult = new EmployeeSearchResultViewModelList(mockEmployees.Take(25), mockEmployees.Count);
        }

        public async Task<EmployeeSearchResultViewModelList> GetEmployeesSalariesAsync()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1200));

            return _stubSearchResult;
        }
    }
}
