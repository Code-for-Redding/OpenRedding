namespace OpenRedding.Core.Data
{
    using System;
    using Domain.Salaries.Dtos;
    using Domain.Salaries.Entities;

    public static class CustomConverters
    {
        public static Func<TransparentCaliforniaCsvReadEmployeeDto, string?> AgencyConverter => row =>
        {
            if (string.Equals(row.EmployeeAgency, "Shasta County", StringComparison.CurrentCultureIgnoreCase))
            {
                return EmployeeAgency.ShastaCounty.ToString();
            }

            return string.Equals(row.EmployeeAgency, "Redding", StringComparison.CurrentCultureIgnoreCase)
                ? EmployeeAgency.Redding.ToString()
                : EmployeeAgency.Other.ToString();
        };

        public static Func<TransparentCaliforniaCsvReadEmployeeDto, string?> StatusConverter => row =>
        {
            if (string.Equals(row.EmployeeStatus, "FT", StringComparison.CurrentCultureIgnoreCase))
            {
                return EmployeeStatus.FullTime.ToString();
            }

            return string.Equals(row.EmployeeStatus, "PT", StringComparison.CurrentCultureIgnoreCase)
                ? EmployeeStatus.PartTime.ToString()
                : EmployeeStatus.Other.ToString();
        };
    }
}