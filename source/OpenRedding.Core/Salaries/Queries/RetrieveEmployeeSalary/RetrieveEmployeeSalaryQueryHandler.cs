namespace OpenRedding.Core.Salaries.Queries.RetrieveEmployeeSalary
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Net;
    using System.Text.RegularExpressions;
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

            // NOTE: As of EF Core 3.0, StringComparison no longer works due to server-side evaluation of queries
            Expression<Func<Employee, bool>> matchingEmployeeNamePredicate = e =>
                !string.IsNullOrWhiteSpace(e.FirstName) && !string.IsNullOrWhiteSpace(e.LastName) && // Ensure the employee name is populated
                e.FirstName.Equals(employeeDetail.FirstName) && // Match on the first name
                e.LastName.Equals(employeeDetail.LastName) && // Match on the last name
                e.Year != employeeDetail.Year; // Exclude the current retrieval year in the result set

            // Retrieve the related records
            var relatedRecords = await _context.Employees
                .Where(matchingEmployeeNamePredicate)
                .Select(e => e.ToRelatedEmployeeDetailDto(request.GatewayUrl))
                .ToListAsync(cancellationToken);

            // Compute base pay and total pay percentiles for job and agency
            Expression<Func<Employee, bool>> matchingJobTitleForFiscalYear = e =>
                    !string.IsNullOrWhiteSpace(e.JobTitle) &&
                    e.JobTitle.Equals(employeeDetail.JobTitle) &&
                    e.Year == employeeDetail.Year;

            var basePayAverage = await _context.Employees
                .Where(matchingJobTitleForFiscalYear)
                .AverageAsync(e => e.BasePay, cancellationToken);

            var benefitsAverage = await _context.Employees
                .Where(matchingJobTitleForFiscalYear)
                .AverageAsync(e => e.Benefits, cancellationToken);

            var totalPayAverage = await _context.Employees
                .Where(matchingJobTitleForFiscalYear)
                .AverageAsync(e => e.TotalPayWithBenefits, cancellationToken);

            return new EmployeeSalaryDetailViewModel
            {
                Employee = employeeDetail.ToEmployeeSalaryDetailDto(request.GatewayUrl),
                RelatedRecords = relatedRecords,
                OccupationalBasePayAverage = basePayAverage,
                OccupationalTotalPayAverage = totalPayAverage,
                OccupationalBenefitsAverage = benefitsAverage
            };
        }
    }
}
