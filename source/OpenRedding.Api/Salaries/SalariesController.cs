namespace OpenRedding.Api.Salaries
{
    using System.Threading.Tasks;
    using Core.Salaries.Queries.GetEmployeeSalaries;
    using Domain.Salaries.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using OpenRedding.Core.Salaries.Queries.RetrieveEmployeeSalary;

    public class SalariesController : OpenReddingBaseController
    {
        private readonly ILogger<SalariesController> _logger;

        public SalariesController(ILogger<SalariesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(EmployeeSearchResultList), StatusCodes.Status200OK)]
        public async Task<EmployeeSearchResultList> GetEmployeeSalaries(
            [FromQuery] string? name,
            [FromQuery] string? jobTitle,
            [FromQuery] string? agency,
            [FromQuery] string? status,
            [FromQuery] string? sortBy)
        {
            var requestPath = Request.Path.ToString();
            _logger.LogInformation($"Querying salaries: name [{name}], jobTitle [{jobTitle}], agency [{agency}], status [{status}], sortBy [{sortBy}]");
            return await Mediator.Send(new GetEmployeeSalariesQuery(name, jobTitle, agency, status, sortBy));
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