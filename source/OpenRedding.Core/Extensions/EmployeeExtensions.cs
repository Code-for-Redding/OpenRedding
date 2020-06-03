namespace OpenRedding.Core.Extensions
{
    using System;
    using System.Net.Http;
    using Domain.Salaries.Dtos;
    using Domain.Salaries.Entities;
    using OpenRedding.Domain.Common.Miscellaneous;
    using OpenRedding.Domain.Salaries.Enums;
    using OpenRedding.Domain.Salaries.ViewModels;
    using OpenRedding.Shared;

    public static class EmployeeExtensions
    {
        /// <summary>
        /// Maps an employee database model to a view model search DTO.
        /// </summary>
        /// <param name="employee">Employee database model.</param>
        /// <param name="gatewayUrl">Base URL for the API gateway to attach the self link for each record.</param>
        /// <returns>Mapped employee search DTO.</returns>
        /// <exception cref="ArgumentNullException">Throws if entity is null for validation.</exception>
        public static EmployeeSalarySearchResultDto ToEmployeeSalarySearchResultDto(this Employee employee, Uri gatewayUrl)
        {
            ArgumentValidation.CheckNotNull(employee, nameof(employee));

            return new EmployeeSalarySearchResultDto(
                employee.EmployeeId,
                GetEmployeeName(employee),
                employee.JobTitle,
                employee.EmployeeAgency.ToFriendlyString(),
                employee.EmployeeStatus.ToFriendlyString(),
                employee.Year,
                employee.BasePay,
                employee.TotalPayWithBenefits,
                GetSelfLink(employee.EmployeeId, gatewayUrl));
        }

        /// <summary>
        /// Maps an employee database model to a view model detail DTO.
        /// </summary>
        /// <param name="employee">Employee database model.</param>
        /// <param name="gatewayUrl">Base URL for the API gateway to attach the self link for each record.</param>
        /// <returns>Mapped employee detail DTO.</returns>
        /// <exception cref="ArgumentNullException">Throws if entity is null for validation.</exception>
        public static EmployeeSalaryDetailDto ToEmployeeSalaryDetailDto(this Employee employee, Uri gatewayUrl)
        {
            ArgumentValidation.CheckNotNull(employee, nameof(employee));

            return new EmployeeSalaryDetailDto
            {
                Id = employee.EmployeeId,
                Name = GetEmployeeName(employee),
                JobTitle = string.IsNullOrWhiteSpace(employee.JobTitle) ? string.Empty : employee.JobTitle,
                BasePay = employee.BasePay,
                Benefits = employee.Benefits,
                OtherPay = employee.OtherPay,
                OvertimePay = employee.OvertimePay,
                TotalPay = employee.TotalPay,
                TotalPayWithBenefits = employee.TotalPayWithBenefits,
                Year = employee.Year,
                Agency = employee.EmployeeAgency.ToString(),
                Status = employee.EmployeeStatus.ToString(),
                Self = GetSelfLink(employee.EmployeeId, gatewayUrl)
            };
        }

        /// <summary>
        /// Maps a the CSV employee model to the employee database model.
        /// </summary>
        /// <param name="employeeDto">CSV model from Transparent California.</param>
        /// <returns>Mapped employee entity.</returns>
        /// <exception cref="ArgumentNullException">Throws if DTO is null for validation.</exception>
        public static Employee ToEmployee(this TransparentCaliforniaCsvReadEmployeeDto employeeDto)
        {
            ArgumentValidation.CheckNotNull(employeeDto, nameof(employeeDto));

            string firstName = string.Empty;
            string middleName = string.Empty;
            string lastName = string.Empty;

            // Parse the employee name to persist
            var tokenizedName = employeeDto.EmployeeName?.Split(" ");

            if (!(tokenizedName is null))
            {
                switch (tokenizedName.Length)
                {
                    case 1:
                        firstName = tokenizedName[0];
                        break;
                    case 2:
                        firstName = tokenizedName[0];
                        lastName = tokenizedName[1];
                        break;
                    case 3:
                        firstName = tokenizedName[0];
                        middleName = tokenizedName[1];
                        lastName = tokenizedName[2];
                        break;
                }
            }

            return new Employee
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                JobTitle = employeeDto.JobTitle,
                BasePay = employeeDto.BasePay,
                OtherPay = employeeDto.OtherPay,
                TotalPay = employeeDto.TotalPay,
                Benefits = employeeDto.Benefits,
                OvertimePay = employeeDto.OvertimePay,
                PensionDebt = employeeDto.PensionDebt ?? decimal.Zero,
                TotalPayWithBenefits = employeeDto.TotalPayWithBenefits,
                EmployeeAgency = OpenReddingEnumConverter.ConvertAgencyFromString(employeeDto.EmployeeAgency),
                EmployeeStatus = OpenReddingEnumConverter.ConvertStatusFromString(employeeDto.EmployeeStatus),
                Notes = employeeDto.Notes,
                Year = employeeDto.Year
            };
        }

        public static RelatedEmployeeDetailDto ToRelatedEmployeeDetailDto(this Employee employee, Uri gatewayUrl)
        {
            ArgumentValidation.CheckNotNull(employee, nameof(employee));

            return new RelatedEmployeeDetailDto
            {
                Name = GetEmployeeName(employee),
                JobTitle = string.IsNullOrWhiteSpace(employee.JobTitle) ? string.Empty : employee.JobTitle,
                Agency = employee.EmployeeAgency.ToFriendlyString(),
                BasePay = employee.BasePay,
                TotalPayWithBenefits = employee.TotalPayWithBenefits,
                Year = employee.Year,
                Self = GetSelfLink(employee.EmployeeId, gatewayUrl)
            };
        }

        public static EmployeeSalaryExportDto ToSalaryExportDto(this Employee employee)
        {
            ArgumentValidation.CheckNotNull(employee, nameof(employee));

            return new EmployeeSalaryExportDto(
                GetEmployeeName(employee),
                string.IsNullOrWhiteSpace(employee.JobTitle) ? string.Empty : employee.JobTitle,
                employee.BasePay,
                employee.OvertimePay,
                employee.OtherPay,
                employee.Benefits,
                employee.TotalPay,
                employee.PensionDebt ?? 0m,
                employee.TotalPayWithBenefits,
                employee.Year,
                employee.EmployeeAgency,
                employee.EmployeeStatus);
        }

        private static string ToFriendlyString(this EmployeeAgency agency)
        {
            return agency switch
            {
                EmployeeAgency.ShastaCounty => "Shasta County",
                _ => agency.ToString()
            };
        }

        private static string ToFriendlyString(this EmployeeStatus status)
        {
            return status switch
            {
                EmployeeStatus.FullTime => "Full-time",
                EmployeeStatus.PartTime => "Part-time",
                _ => status.ToString()
            };
        }

        private static OpenReddingLink GetSelfLink(int id, Uri gatewayUrl)
        {
            return new OpenReddingLink
            {
                Href = $"{gatewayUrl.AbsoluteUri}/salaries/{id}",
                Rel = nameof(EmployeeSalaryDetailViewModel),
                Method = HttpMethod.Get.Method
            };
        }

        private static string GetEmployeeName(Employee employee)
        {
            return string.IsNullOrWhiteSpace(employee.MiddleName) ?
                $"{employee.FirstName} {employee.LastName}" :
                $"{employee.FirstName} {employee.MiddleName} {employee.LastName}";
        }
    }
}
