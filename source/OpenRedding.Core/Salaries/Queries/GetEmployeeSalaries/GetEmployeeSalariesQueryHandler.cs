namespace OpenRedding.Core.Salaries.Queries.GetEmployeeSalaries
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Domain.Salaries.Entities;
    using Domain.Salaries.ViewModels;
    using Extensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OpenRedding.Domain.Common.Dto;
    using OpenRedding.Domain.Salaries.Queries;
    using Shared;

    public class GetEmployeeSalariesQueryHandler : IRequestHandler<GetEmployeeSalariesQuery, EmployeeSearchResultList>
    {
        private readonly IOpenReddingDbContext _context;

        public GetEmployeeSalariesQueryHandler(IOpenReddingDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeSearchResultList> Handle(GetEmployeeSalariesQuery request, CancellationToken cancellationToken)
        {
            ArgumentValidation.ValidateNotNull(request);
            ArgumentValidation.CheckNotNull(request, nameof(request));

            // Request context to not track entity since we're not making any updates
            var queriedSalaries = _context.Employees.AsNoTracking();

            // Filter by job title, if available
            if (!string.IsNullOrWhiteSpace(request.JobTitle))
            {
                // NOTE: As of EF Core 3.0, StringComparison no longer works due to server-side evaulation of queries
                Expression<Func<Employee, bool>> canFilterByEmployeeJobTitle = e =>
                    !string.IsNullOrWhiteSpace(e.JobTitle) && e.JobTitle.Contains(request.JobTitle);

                queriedSalaries = queriedSalaries.Where(canFilterByEmployeeJobTitle);
            }

            // Filter by name, if available
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                // NOTE: As of EF Core 3.0, StringComparison no longer works due to server-side evaulation of queries
                Expression<Func<Employee, bool>> canFilterByEmployeeName = e =>
                    !string.IsNullOrWhiteSpace(e.EmployeeName) && e.EmployeeName.Contains(request.Name);

                queriedSalaries = queriedSalaries.Where(canFilterByEmployeeName);
            }

            // Filter by agency, if available
            if (!string.IsNullOrWhiteSpace(request.Agency) && Enum.TryParse(request.Agency, true, out EmployeeAgency employeeAgency))
            {
                queriedSalaries = queriedSalaries.Where(e => e.EmployeeAgency == employeeAgency);
            }

            // Filter by status, if available
            if (!string.IsNullOrWhiteSpace(request.Status) && Enum.TryParse(request.Status, true, out EmployeeStatus employeeStatus))
            {
                queriedSalaries = queriedSalaries.Where(e => e.EmployeeStatus == employeeStatus);
            }

            if (!string.IsNullOrWhiteSpace(request.SortBy) && Enum.TryParse(request.SortBy, true, out OpenReddingSortOption sortOption))
            {
                queriedSalaries = sortOption switch
                {
                    OpenReddingSortOption.AscendingBaseSalary => queriedSalaries.OrderBy(e => e.BasePay),
                    OpenReddingSortOption.DescendingBaseSalary => queriedSalaries.OrderByDescending(e => e.BasePay),
                    OpenReddingSortOption.AscendingJobTitle => queriedSalaries.OrderBy(e => e.JobTitle),
                    OpenReddingSortOption.DescendingJobTitle => queriedSalaries.OrderByDescending(e => e.JobTitle),
                    OpenReddingSortOption.AscendingName => queriedSalaries.OrderBy(e => e.EmployeeName),
                    OpenReddingSortOption.DescendingName => queriedSalaries.OrderByDescending(e => e.EmployeeName),
                    OpenReddingSortOption.AscendingTotalSalary => queriedSalaries.OrderBy(e => e.TotalPayWithBenefits),
                    OpenReddingSortOption.DescendingTotalSalary => queriedSalaries.OrderByDescending(e => e.TotalPayWithBenefits),
                    _ => queriedSalaries
                };
            }

            // Perform the query and map each resulting record to its search DTO
            var totalResults = queriedSalaries.Count();
            var resultingSalaries = await queriedSalaries
                .SkipAndTakeDefault(request.Page)
                .Select(e => e.ToEmployeeSalarySearchDto())
                .ToListAsync(cancellationToken);

            return new EmployeeSearchResultList(resultingSalaries, totalResults);
        }
    }
}