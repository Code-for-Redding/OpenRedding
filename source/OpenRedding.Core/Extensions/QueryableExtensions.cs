namespace OpenRedding.Core.Extensions
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.Entities;
    using OpenRedding.Domain.Salaries.Enums;
    using OpenRedding.Shared;

    public static class QueryableExtensions
    {
        public static IQueryable<T> SkipAndTakeDefault<T>(this IQueryable<T> queryable, int page)
            where T : class
        {
            if (queryable is null)
            {
                throw new ArgumentNullException(nameof(queryable), "Queryable cannot be null, please check LINQ statement");
            }

            return queryable
                .Skip(OpenReddingConstants.MaxPageSizeResult * (page - 1))
                .Take(OpenReddingConstants.MaxPageSizeResult);
        }

        public static IQueryable<Employee> FromSearchRequest(this IQueryable<Employee> queriedSalaries, EmployeeSalarySearchRequestDto searchRequest)
        {
            if (queriedSalaries is null)
            {
                throw new ArgumentNullException(nameof(queriedSalaries), "Queryable cannot be null, please check LINQ statement");
            }

            // Filter by job title, if available
            if (!string.IsNullOrWhiteSpace(searchRequest.JobTitle))
            {
                // NOTE: As of EF Core 3.0, StringComparison no longer works due to server-side evaluation of queries
                Expression<Func<Employee, bool>> canFilterByEmployeeJobTitle = e =>
                    !string.IsNullOrWhiteSpace(e.JobTitle) && e.JobTitle.Contains(searchRequest.JobTitle);

                queriedSalaries = queriedSalaries.Where(canFilterByEmployeeJobTitle);
            }

            // Filter by name, if available
            if (!string.IsNullOrWhiteSpace(searchRequest.Name))
            {
                // NOTE: As of EF Core 3.0, StringComparison no longer works due to server-side evaluation of queries
                Expression<Func<Employee, bool>> canFilterByEmployeeName = e =>
                    !string.IsNullOrWhiteSpace(e.FirstName) && !string.IsNullOrWhiteSpace(e.LastName) &&
                    (e.FirstName.Contains(searchRequest.Name) || e.LastName.Contains(searchRequest.Name));

                queriedSalaries = queriedSalaries.Where(canFilterByEmployeeName);
            }

            // Filter by year, if available
            if (searchRequest.Year.HasValue)
            {
                var parsedEmploymentYear = (EmploymentYear)searchRequest.Year.Value;

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
                    EmploymentYear._2019 => queriedSalaries.Where(e => e.Year == 2019),
                    _ => queriedSalaries
                };
            }

            // Filter by status, if available
            if (!string.IsNullOrWhiteSpace(searchRequest.Status) && Enum.TryParse(searchRequest.Status, true, out EmployeeStatus employeeStatus))
            {
                queriedSalaries = employeeStatus switch
                {
                    EmployeeStatus.FullTime => queriedSalaries.Where(e => e.EmployeeStatus == employeeStatus),
                    EmployeeStatus.PartTime => queriedSalaries.Where(e => e.EmployeeStatus == employeeStatus),
                    _ => queriedSalaries
                };
            }

            // Filter by agency, if available
            if (!string.IsNullOrWhiteSpace(searchRequest.Agency) && Enum.TryParse(searchRequest.Agency, true, out EmployeeAgency employeeAgency))
            {
                queriedSalaries = employeeAgency switch
                {
                    EmployeeAgency.Redding => queriedSalaries.Where(e => e.EmployeeAgency == employeeAgency),
                    EmployeeAgency.ShastaCounty => queriedSalaries.Where(e => e.EmployeeAgency == employeeAgency),
                    _ => queriedSalaries
                };
            }

            // Filter by base pay range, if available
            if (searchRequest.BasePayRange.HasValue && Enum.IsDefined(typeof(SalarySearchRange), searchRequest.BasePayRange.Value))
            {
                var parsedRange = (SalarySearchRange)searchRequest.BasePayRange.Value;

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
            if (searchRequest.TotalPayRange.HasValue && Enum.IsDefined(typeof(SalarySearchRange), searchRequest.TotalPayRange.Value))
            {
                var parsedRange = (SalarySearchRange)searchRequest.TotalPayRange.Value;

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
            if (!string.IsNullOrWhiteSpace(searchRequest.SortField) && Enum.TryParse(searchRequest.SortField, true, out SalarySortField sortOption))
            {
                var sortBy = SalarySortByOption.Default;

                // Check for the sort order
                if (Enum.TryParse(searchRequest.SortBy, true, out SalarySortByOption option))
                {
                    sortBy = option;
                }

                queriedSalaries = sortBy switch
                {
                    SalarySortByOption.Ascending => sortOption switch
                    {
                        SalarySortField.BaseSalary => queriedSalaries.OrderBy(e => e.BasePay),
                        SalarySortField.JobTitle => queriedSalaries.OrderBy(e => e.JobTitle),
                        SalarySortField.Name => queriedSalaries.OrderBy(e => e.LastName),
                        SalarySortField.Year => queriedSalaries.OrderBy(e => e.Year),
                        SalarySortField.TotalWithBenefitsSalary => queriedSalaries.OrderBy(e => e.TotalPayWithBenefits),
                        _ => queriedSalaries
                    },
                    SalarySortByOption.Descending => sortOption switch
                    {
                        SalarySortField.BaseSalary => queriedSalaries.OrderByDescending(e => e.BasePay),
                        SalarySortField.JobTitle => queriedSalaries.OrderByDescending(e => e.JobTitle),
                        SalarySortField.Name => queriedSalaries.OrderByDescending(e => e.LastName),
                        SalarySortField.Year => queriedSalaries.OrderByDescending(e => e.Year),
                        SalarySortField.TotalWithBenefitsSalary => queriedSalaries.OrderByDescending(e => e.TotalPayWithBenefits),
                        _ => queriedSalaries
                    },
                    _ => queriedSalaries
                };
            }

            return queriedSalaries;
        }
    }
}
