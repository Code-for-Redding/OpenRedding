namespace OpenRedding.Core.Extensions
{
    using System;
    using OpenRedding.Domain.Salaries.Enums;

    public static class OpenReddingEnumConverter
    {
        public static EmployeeStatus ConvertStatusFromString(string? status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return EmployeeStatus.Unknown;
            }

            if (string.Equals(status, "FT", StringComparison.CurrentCultureIgnoreCase))
            {
                return EmployeeStatus.FullTime;
            }

            return string.Equals(status, "PT", StringComparison.CurrentCultureIgnoreCase)
                ? EmployeeStatus.PartTime
                : EmployeeStatus.Other;
        }

        public static EmployeeAgency ConvertAgencyFromString(string? agency)
        {
            if (string.IsNullOrWhiteSpace(agency))
            {
                return EmployeeAgency.Unknown;
            }

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
