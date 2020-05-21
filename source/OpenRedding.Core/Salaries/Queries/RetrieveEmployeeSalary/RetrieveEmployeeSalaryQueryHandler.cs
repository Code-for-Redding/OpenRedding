namespace OpenRedding.Core.Salaries.Queries.RetrieveEmployeeSalary
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Domain.Salaries.ViewModels;
    using Exception;
    using Extensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OpenRedding.Domain.Salaries.Entities;
    using OpenRedding.Shared.Validation;

    public class RetrieveEmployeeSalaryQueryHandler : IRequestHandler<RetrieveEmployeeSalaryQuery, EmployeeSalaryDetailViewModel>
    {
        private readonly IOpenReddingDbContext _context;

        public RetrieveEmployeeSalaryQueryHandler(IOpenReddingDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeSalaryDetailViewModel> Handle(RetrieveEmployeeSalaryQuery request, CancellationToken cancellationToken)
        {
            Validate.NotNull(request, nameof(request));

            var employeeDetail = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == request.Id, cancellationToken);

            if (employeeDetail is null)
            {
                throw new OpenReddingApiException($"Employee with ID {request?.Id} was not found", HttpStatusCode.NotFound);
            }

            // Find related records to display on the view model as links for clients to retrieve
            // Split the employee name by spaces to get first and last name, ignore matching on middle names
            var tokenizedName = employeeDetail.EmployeeName?.Split(" ");

            // Name was null or not complete, return the current state
            if (tokenizedName is null || tokenizedName.Length is 1)
            {
                return new EmployeeSalaryDetailViewModel(employeeDetail.ToEmployeeSalaryDetailDto(request.GatewayUrl));
            }

            var firstName = tokenizedName[0];
            var lastName = tokenizedName.Length > 2 ? tokenizedName[2] : tokenizedName[1];

            // NOTE: As of EF Core 3.0, StringComparison no longer works due to server-side evaluation of queries
            Expression<Func<Employee, bool>> matchingEmployeeNamePredicate = e =>
                    !string.IsNullOrWhiteSpace(e.EmployeeName) && // Ensure the employee name is populated
                    e.EmployeeName.Contains(firstName) && e.EmployeeName.Contains(lastName) && // Match on the first and last name
                    e.Year != employeeDetail.Year; // Exclude the current retrieval year in the result set

            var relatedRecords = await _context.Employees
                .Where(matchingEmployeeNamePredicate)
                .Select(e => e.ToRelatedEmployeeDetailDto(request.GatewayUrl))
                .ToListAsync(cancellationToken);

            return new EmployeeSalaryDetailViewModel(employeeDetail.ToEmployeeSalaryDetailDto(request.GatewayUrl), relatedRecords);
        }
    }
}
