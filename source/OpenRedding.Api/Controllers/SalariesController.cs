namespace OpenRedding.Api.Controllers
{
    using System.Threading.Tasks;
    using Domain.Salaries.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Api;
    using OpenRedding.Core.Salaries.Queries.GetEmployeeSalaries;
    using OpenRedding.Core.Salaries.Queries.RetrieveEmployeeSalary;
    using OpenRedding.Domain.Salaries.Dtos;

    public class SalariesController : OpenReddingBaseController
    {
        private readonly ILogger<SalariesController> _logger;

        public SalariesController(ILogger<SalariesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(EmployeeSearchResultViewModelList), StatusCodes.Status200OK)]
        public async Task<EmployeeSearchResultViewModelList> GetEmployeeSalaries(
            [FromQuery] string? name,
            [FromQuery] string? jobTitle,
            [FromQuery] string? agency,
            [FromQuery] string? status,
            [FromQuery] string? sortBy,
            [FromQuery] int? year,
            [FromQuery] int? page)
        {
            _logger.LogInformation($"Querying salaries: name [{name}], jobTitle [{jobTitle}], agency [{agency}], status [{status}], sortBy [{sortBy}]");
            var searchRequest = new EmployeeSalarySearchRequestDto(name, jobTitle, agency, status, sortBy, year);
            return await Mediator.Send(new GetEmployeeSalariesQuery(searchRequest, page));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmployeeSalaryDetailViewModel), StatusCodes.Status200OK)]
        public async Task<EmployeeSalaryDetailViewModel> GetEmployeeSalary([FromRoute] int id)
        {
            _logger.LogInformation($"Retrieving employee salary detail for employeeId [{id}]");
            return await Mediator.Send(new RetrieveEmployeeSalaryQuery(id));
        }
    }
}
