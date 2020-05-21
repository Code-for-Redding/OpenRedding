namespace OpenRedding.Core.Salaries.Queries.GetEmployeeSalaries
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Domain.Salaries.Entities;
    using Extensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OpenRedding.Domain.Common.Aggregates;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.Enums;
    using Shared;

    public class GetEmployeeSalariesQueryHandler : IRequestHandler<GetEmployeeSalariesQuery, OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>
    {
        private readonly IOpenReddingDbContext _context;

        public GetEmployeeSalariesQueryHandler(IOpenReddingDbContext context) =>
            _context = context;

        public async Task<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>> Handle(GetEmployeeSalariesQuery request, CancellationToken cancellationToken)
        {
            ArgumentValidation.CheckNotNull(request, nameof(request));
            ArgumentValidation.CheckNotNull(request.SearchRequest, nameof(request.SearchRequest));

            // Request context to not track entity since we're not making any updates
            var queriedSalaries = _context.Employees.AsNoTracking();

            // Filter by job title, if available
            if (!string.IsNullOrWhiteSpace(request.SearchRequest.JobTitle))
            {
                // NOTE: As of EF Core 3.0, StringComparison no longer works due to server-side evaluation of queries
                Expression<Func<Employee, bool>> canFilterByEmployeeJobTitle = e =>
                    !string.IsNullOrWhiteSpace(e.JobTitle) && e.JobTitle.Contains(request.SearchRequest.JobTitle);

                queriedSalaries = queriedSalaries.Where(canFilterByEmployeeJobTitle);
            }

            // Filter by name, if available
            if (!string.IsNullOrWhiteSpace(request.SearchRequest.Name))
            {
                // NOTE: As of EF Core 3.0, StringComparison no longer works due to server-side evaluation of queries
                Expression<Func<Employee, bool>> canFilterByEmployeeName = e =>
                    !string.IsNullOrWhiteSpace(e.EmployeeName) && e.EmployeeName.Contains(request.SearchRequest.Name);

                queriedSalaries = queriedSalaries.Where(canFilterByEmployeeName);
            }

            // Filter by agency, if available
            if (!string.IsNullOrWhiteSpace(request.SearchRequest.Agency) && Enum.TryParse(request.SearchRequest.Agency, true, out EmployeeAgency employeeAgency))
            {
                queriedSalaries = queriedSalaries.Where(e => e.EmployeeAgency == employeeAgency);
            }

            // Filter by year, if available
            if (request.SearchRequest.Year.HasValue)
            {
                var parsedEmploymentYear = (EmploymentYear)request.SearchRequest.Year.Value;

                queriedSalaries = parsedEmploymentYear switch
                {
                    EmploymentYear._2011 => queriedSalaries.Where(e => e.Year == 2011),
                    EmploymentYear._2012 => queriedSalaries.Where(e => e.Year == 2012),
                    EmploymentYear._2013 => queriedSalaries.Where(e => e.Year == 2013),
                    EmploymentYear._2014 => queriedSalaries.Where(e => e.Year == 2014),
                    EmploymentYear._2015 => queriedSalaries.Where(e => e.Year == 2015),
                    EmploymentYear._2016 => queriedSalaries.Where(e => e.Year == 2016),
                    EmploymentYear._2017 => queriedSalaries.Where(e => e.Year == 2017),
                    EmploymentYear._2018 => queriedSalaries.Where(e => e.Year == 2018),
                    _ => queriedSalaries
                };
            }

            // Filter by status, if available
            if (!string.IsNullOrWhiteSpace(request.SearchRequest.Status) && Enum.TryParse(request.SearchRequest.Status, true, out EmployeeStatus employeeStatus))
            {
                queriedSalaries = queriedSalaries.Where(e => e.EmployeeStatus == employeeStatus);
            }

            // Filter by base pay range, if available
            if (request.SearchRequest.BasePayRange.HasValue && Enum.IsDefined(typeof(SalarySearchRange), request.SearchRequest.BasePayRange.Value))
            {
                var parsedRange = (SalarySearchRange)request.SearchRequest.BasePayRange.Value;

                queriedSalaries = parsedRange switch
                {
                    SalarySearchRange._0To49 => queriedSalaries.Where(e => e.BasePay >= 0 && e.BasePay < 50000m),
                    SalarySearchRange._50To100 => queriedSalaries.Where(e => e.BasePay >= 50000m && e.BasePay < 100000m),
                    SalarySearchRange._100To149 => queriedSalaries.Where(e => e.BasePay >= 100000m && e.BasePay < 150000m),
                    SalarySearchRange._150To199 => queriedSalaries.Where(e => e.BasePay >= 150000m && e.BasePay < 200000m),
                    SalarySearchRange._200To249 => queriedSalaries.Where(e => e.BasePay >= 200000m && e.BasePay < 250000m),
                    SalarySearchRange._250To299 => queriedSalaries.Where(e => e.BasePay >= 250000m && e.BasePay < 300000m),
                    SalarySearchRange._300AndGreater => queriedSalaries.Where(e => e.BasePay >= 300000),
                    _ => queriedSalaries
                };
            }

            // Filter by total pay with benefits range, if available
            if (request.SearchRequest.TotalPayRange.HasValue && Enum.IsDefined(typeof(SalarySearchRange), request.SearchRequest.TotalPayRange.Value))
            {
                var parsedRange = (SalarySearchRange)request.SearchRequest.TotalPayRange.Value;

                queriedSalaries = parsedRange switch
                {
                    SalarySearchRange._0To49 => queriedSalaries.Where(e => e.TotalPayWithBenefits >= 0 && e.TotalPayWithBenefits < 50000m),
                    SalarySearchRange._50To100 => queriedSalaries.Where(e => e.TotalPayWithBenefits >= 50000m && e.TotalPayWithBenefits < 100000m),
                    SalarySearchRange._100To149 => queriedSalaries.Where(e => e.TotalPayWithBenefits >= 100000m && e.TotalPayWithBenefits < 150000m),
                    SalarySearchRange._150To199 => queriedSalaries.Where(e => e.TotalPayWithBenefits >= 150000m && e.TotalPayWithBenefits < 200000m),
                    SalarySearchRange._200To249 => queriedSalaries.Where(e => e.TotalPayWithBenefits >= 200000m && e.TotalPayWithBenefits < 250000m),
                    SalarySearchRange._250To299 => queriedSalaries.Where(e => e.TotalPayWithBenefits >= 250000m && e.TotalPayWithBenefits < 300000m),
                    SalarySearchRange._300AndGreater => queriedSalaries.Where(e => e.TotalPayWithBenefits >= 300000),
                    _ => queriedSalaries
                };
            }

            // Sort by the request field, if available
            if (!string.IsNullOrWhiteSpace(request.SearchRequest.SortField) && Enum.TryParse(request.SearchRequest.SortField, true, out SalarySortField sortOption))
            {
                var sortBy = SalarySortByOption.Default;

                // Check for the sort order
                if (Enum.TryParse(request.SearchRequest.SortBy, true, out SalarySortByOption option))
                {
                    sortBy = option;
                }

                queriedSalaries = sortBy switch
                {
                    SalarySortByOption.Ascending => sortOption switch
                    {
                        SalarySortField.BaseSalary => queriedSalaries.OrderBy(e => e.BasePay),
                        SalarySortField.JobTitle => queriedSalaries.OrderBy(e => e.JobTitle),
                        SalarySortField.Name => queriedSalaries.OrderBy(e => e.EmployeeName),
                        SalarySortField.Year => queriedSalaries.OrderBy(e => e.Year),
                        SalarySortField.TotalWithBenefitsSalary => queriedSalaries.OrderBy(e => e.TotalPayWithBenefits),
                        _ => queriedSalaries
                    },
                    SalarySortByOption.Descending => sortOption switch
                    {
                        SalarySortField.BaseSalary => queriedSalaries.OrderByDescending(e => e.BasePay),
                        SalarySortField.JobTitle => queriedSalaries.OrderByDescending(e => e.JobTitle),
                        SalarySortField.Name => queriedSalaries.OrderByDescending(e => e.EmployeeName),
                        SalarySortField.Year => queriedSalaries.OrderByDescending(e => e.Year),
                        SalarySortField.TotalWithBenefitsSalary => queriedSalaries.OrderByDescending(e => e.TotalPayWithBenefits),
                        _ => queriedSalaries
                    },
                    _ => queriedSalaries
                };
            }

            // Perform the query and map each resulting record to its search DTO
            var totalResults = queriedSalaries.Count();
            var resultingSalaries = await queriedSalaries
                .SkipAndTakeDefault(request.Page)
                .Select(e => e.ToEmployeeSalarySearchResultDto(request.GatewayBaseUrl))
                .ToListAsync(cancellationToken);

            return new OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>(resultingSalaries.AsEnumerable(), totalResults, request.Page);
        }
    }
}
