namespace OpenRedding.Api.Salaries
{
    using System.Threading.Tasks;
    using Core.Salaries.Queries.GetEmployeeSalaries;
    using Domain.Salaries.ViewModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

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
            [FromQuery] string? status)
        {
            _logger.LogInformation($"Querying salaries: name [{name}], jobTitle [{jobTitle}], agency [{agency}], status [{status}]");
            return await Mediator.Send(new GetEmployeeSalariesQuery(name, jobTitle, agency, status));
        }
    }
}