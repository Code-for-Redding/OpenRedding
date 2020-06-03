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

            // Find related records to display on the view model as links for clients to retrieve
            // Split the employee name by spaces to get first and last name, ignore matching on middle names
            var tokenizedName = employeeDetail.EmployeeName?.Split(" ");

            // Name was null or not complete, return the current state
            if (tokenizedName is null || tokenizedName.Length is 1)
            {
                return new EmployeeSalaryDetailViewModel
                {
                    Employee = employeeDetail.ToEmployeeSalaryDetailDto(request.GatewayUrl)
                };
            }

            // Grab a reference to the first and last name, discarding the middle name (if populated)
            var firstName = tokenizedName[0];
            var lastName = tokenizedName.Length > 2 ? tokenizedName[2] : tokenizedName[1];

            // Search on names that contain both the first and last, as well as all three parts
            var searchName = $"{firstName} {lastName}";
            Expression<Func<Employee, bool>> matchingEmployeeNamePredicate;

            if (tokenizedName.Length > 2)
            {
                matchingEmployeeNamePredicate = e =>
                    !string.IsNullOrWhiteSpace(e.EmployeeName) && // Ensure the employee name is populated
                    (e.EmployeeName.Equals(employeeDetail.EmployeeName) || e.EmployeeName.Equals(searchName)) && // Match on the first and last name, or first/last/middle name
                    e.Year != employeeDetail.Year; // Exclude the current retrieval year in the result set
            }
            else
            {
                // Use the original employee name if no middle name is provided
                matchingEmployeeNamePredicate = e =>
                    !string.IsNullOrWhiteSpace(e.EmployeeName) && // Ensure the employee name is populated
                    e.EmployeeName.Equals(employeeDetail.EmployeeName) && // Match on the first and last name
                    e.Year != employeeDetail.Year; // Exclude the current retrieval year in the result set
            }

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
