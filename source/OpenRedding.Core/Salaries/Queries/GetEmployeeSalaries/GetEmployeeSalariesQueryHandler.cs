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

            // Request context to not track entity since we're not making any updates
            var queriedSalaries = _context.Employees.AsNoTracking();

            // Filter by job title, if available
            if (!string.IsNullOrWhiteSpace(request.JobTitle))
            {
                Expression<Func<Employee, bool>> canFilterByEmployeeJobTitle = e =>
                    !string.IsNullOrWhiteSpace(e.JobTitle) &&
                    e.JobTitle.Contains(request.JobTitle, StringComparison.CurrentCultureIgnoreCase);

                queriedSalaries = queriedSalaries.Where(canFilterByEmployeeJobTitle);
            }

            // Filter by name, if available
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                Expression<Func<Employee, bool>> canFilterByEmployeeName = e =>
                    !string.IsNullOrWhiteSpace(e.JobTitle) &&
                    e.JobTitle.Contains(request.Name, StringComparison.CurrentCultureIgnoreCase);

                queriedSalaries = queriedSalaries.Where(canFilterByEmployeeName);
            }

            // Filter by agency ,if available
            if (!string.IsNullOrWhiteSpace(request.Agency) && Enum.TryParse(request.Agency, true, out EmployeeAgency employeeAgency))
            {
                queriedSalaries = queriedSalaries.Where(e => e.EmployeeAgency == employeeAgency);
            }

            // Filter by status ,if available
            if (!string.IsNullOrWhiteSpace(request.Status) && Enum.TryParse(request.Status, true, out EmployeeStatus employeeStatus))
            {
                queriedSalaries = queriedSalaries.Where(e => e.EmployeeStatus == employeeStatus);
            }

            // Perform the query and map each resulting record to its search DTO
            var resultingSalaries = await queriedSalaries
                .Select(e => e.ToEmployeeSalarySearchDto())
                .ToListAsync(cancellationToken);

            return new EmployeeSearchResultList(resultingSalaries);
        }
    }
}