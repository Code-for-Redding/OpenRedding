namespace OpenRedding.Infrastructure.Mappings
{
    using System;
    using Domain.Salaries.Dtos;
    using Domain.Salaries.Entities;

    public static class CustomConverters
    {
        public static Func<TransparentCaliforniaCsvReadEmployeeDto, string> AgencyConverter => row =>
        {
            if (string.Equals(row.EmployeeAgency, "Shasta County", StringComparison.CurrentCultureIgnoreCase))
            {
                return nameof(EmployeeAgency.ShastaCounty);
            }

            return string.Equals(row.EmployeeAgency, "Redding", StringComparison.CurrentCultureIgnoreCase)
                ? nameof(EmployeeAgency.Redding)
                : nameof(EmployeeAgency.Other);
        };

        public static Func<TransparentCaliforniaCsvReadEmployeeDto, string> StatusConverter => row =>
        {
            if (string.Equals(row.EmployeeStatus, "FT", StringComparison.CurrentCultureIgnoreCase))
            {
                return nameof(EmployeeStatus.FullTime);
            }

            return string.Equals(row.EmployeeStatus, "PT", StringComparison.CurrentCultureIgnoreCase)
                ? nameof(EmployeeStatus.PartTime)
                : nameof(EmployeeStatus.Other);
        };

        public static EmployeeStatus ConvertStatusFromString(string status)
        {
            if (string.Equals(status, "FT", StringComparison.CurrentCultureIgnoreCase))
            {
                return EmployeeStatus.FullTime;
            }

            return string.Equals(status, "PT", StringComparison.CurrentCultureIgnoreCase)
                ? EmployeeStatus.PartTime
                : EmployeeStatus.Other;
        }

        public static EmployeeAgency ConvertAgencyFromString(string agency)
        {
            if (string.Equals(agency, "Shasta County", StringComparison.CurrentCultureIgnoreCase))
            {
                return EmployeeAgency.ShastaCounty;
            }

            return string.Equals(agency, "Redding", StringComparison.CurrentCultureIgnoreCase)
                ? EmployeeAgency.Redding
                : EmployeeAgency.Other;
        }
    }
}
