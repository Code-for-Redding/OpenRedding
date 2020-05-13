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
                queriedSalaries = employeeAgency switch
                {
                    EmployeeAgency.Redding => queriedSalaries.Where(e => e.EmployeeAgency == employeeAgency),
                    EmployeeAgency.ShastaCounty => queriedSalaries.Where(e => e.EmployeeAgency == employeeAgency),
                    _ => queriedSalaries
                };
            }

            // Filter by year, if available
            if (request.SearchRequest.Year.HasValue)
            {
                queriedSalaries = queriedSalaries.Where(e => e.Year == request.SearchRequest.Year.Value);
            }

            // Filter by status, if available
            if (!string.IsNullOrWhiteSpace(request.SearchRequest.Status) && Enum.TryParse(request.SearchRequest.Status, true, out EmployeeStatus employeeStatus))
            {
                queriedSalaries = employeeStatus switch
                {
                    EmployeeStatus.FullTime => queriedSalaries.Where(e => e.EmployeeStatus == employeeStatus),
                    EmployeeStatus.PartTime => queriedSalaries.Where(e => e.EmployeeStatus == employeeStatus),
                    _ => queriedSalaries
                };
            }

            if (!string.IsNullOrWhiteSpace(request.SearchRequest.SortField) && Enum.TryParse(request.SearchRequest.SortField, true, out SalarySortOption sortOption))
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
                        SalarySortOption.BaseSalary => queriedSalaries.OrderBy(e => e.BasePay),
                        SalarySortOption.JobTitle => queriedSalaries.OrderBy(e => e.JobTitle),
                        SalarySortOption.Name => queriedSalaries.OrderBy(e => e.EmployeeName),
                        SalarySortOption.Year => queriedSalaries.OrderBy(e => e.Year),
                        SalarySortOption.TotalWithBenefitsSalary => queriedSalaries.OrderBy(e => e.TotalPayWithBenefits),
                        _ => queriedSalaries
                    },
                    SalarySortByOption.Descending => sortOption switch
                    {
                        SalarySortOption.BaseSalary => queriedSalaries.OrderByDescending(e => e.BasePay),
                        SalarySortOption.JobTitle => queriedSalaries.OrderByDescending(e => e.JobTitle),
                        SalarySortOption.Name => queriedSalaries.OrderByDescending(e => e.EmployeeName),
                        SalarySortOption.Year => queriedSalaries.OrderByDescending(e => e.Year),
                        SalarySortOption.TotalWithBenefitsSalary => queriedSalaries.OrderByDescending(e => e.TotalPayWithBenefits),
                        _ => queriedSalaries
                    },
                    _ => queriedSalaries
                };
            }

            // Perform the query and map each resulting record to its search DTO
            var totalResults = queriedSalaries.Count();
            var resultingSalaries = await queriedSalaries
                .SkipAndTakeDefault(request.Page)
                .Select(e => e.ToEmployeeSalarySearchResultDto())
                .ToListAsync(cancellationToken);

            return new OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>(resultingSalaries.AsEnumerable(), totalResults, request.Page);
        }
    }
}
