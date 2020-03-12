namespace OpenRedding.Core.Extensions
{
    using System;
    using Domain.Salaries.Dtos;
    using Domain.Salaries.Entities;

    public static class EmployeeExtensions
    {
        /// <summary>
        /// Maps an employee database model to a view model search DTO.
        /// </summary>
        /// <param name="employee">Employee database model.</param>
        /// <returns>Mapped employee search DTO.</returns>
        /// <exception cref="ArgumentNullException">Throws if entity is null for validation.</exception>
        public static EmployeeSalarySearchDto ToEmployeeSalarySearchDto(this Employee employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee), "Employee to map from cannot be null");
            }

            return new EmployeeSalarySearchDto(
                employee.EmployeeId,
                employee.EmployeeName,
                employee.JobTitle,
                nameof(employee.EmployeeAgency),
                nameof(employee.EmployeeStatus),
                employee.BasePay,
                employee.TotalPayWithBenefits);
        }

        /// <summary>
        /// Maps an employee database model to a view model detail DTO.
        /// </summary>
        /// <param name="employee">Employee database model.</param>
        /// <returns>Mapped employee detail DTO.</returns>
        /// <exception cref="ArgumentNullException">Throws if entity is null for validation.</exception>
        public static EmployeeSalaryDetailDto ToEmployeeSalaryDetailDto(this Employee employee)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee), "Employee to map from cannot be null");
            }

            return new EmployeeSalaryDetailDto
            {
                Id = employee.EmployeeId,
                Name = employee.EmployeeName,
                JobTitle = employee.JobTitle,
                BasePay = employee.BasePay,
                Benefits = employee.Benefits,
                OtherPay = employee.OtherPay,
                OvertimePay = employee.OvertimePay,
                TotalPay = employee.TotalPay,
                TotalPayWithBenefits = employee.TotalPayWithBenefits,
                Year = employee.Year,
                Agency = nameof(employee.EmployeeAgency),
                Notes = employee.Notes,
                Status = nameof(employee.EmployeeStatus)
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
            if (employeeDto is null)
            {
                throw new ArgumentNullException(nameof(employeeDto), "CSV Employee to map from cannot be null");
            }

            return new Employee
            {
                EmployeeName = employeeDto.EmployeeName,
                JobTitle = employeeDto.JobTitle,
                BasePay = employeeDto.BasePay,
                OtherPay = employeeDto.OtherPay,
                TotalPay = employeeDto.TotalPay,
                Benefits = employeeDto.Benefits,
                OvertimePay = employeeDto.OvertimePay,
                PensionDebt = employeeDto.PensionDebt ?? decimal.Zero,
                TotalPayWithBenefits = employeeDto.TotalPayWithBenefits,
                EmployeeAgency = EmployeeAgency.Other,
                EmployeeStatus = EmployeeStatus.Other,
                Notes = employeeDto.Notes,
                Year = employeeDto.Year
            };
        }
    }
}
