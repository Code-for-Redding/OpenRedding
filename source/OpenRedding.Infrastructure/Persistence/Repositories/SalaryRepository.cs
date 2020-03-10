namespace OpenRedding.Infrastructure.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using Dapper;
    using OpenRedding.Core.Data;
    using OpenRedding.Domain.Salaries.Entities;

    public class SalaryRepository : ISalaryRepository
    {
        private readonly IDbTransaction _dbTransaction;
        private readonly IDbConnection _dbConnection;

        public SalaryRepository(IDbTransaction dbTransaction)
        {
            _dbTransaction = dbTransaction ?? throw new ArgumentNullException(nameof(dbTransaction), "Transaction cannot be null, please validate your connection string");
            _dbConnection = _dbTransaction.Connection;
        }

        public async Task<int> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken)
        {
            const string insertSql = @"
INSERT INTO OpenRedding.dbo.Employees
(
    EmployeeName,
    JobTitle,
    BasePay,
    Benefits,
    OtherPay,
    OvertimePay,
    PensionDebt,
    TotalPay,
    TotalPayWithBenefits,
    Year,
    EmployeeAgency,
    EmployeeStatus
)
VALUES
(
    @EmployeeName,
    @JobTitle,
    @BasePay,
    @Benefits,
    @OtherPay,
    @OvertimePay,
    @PensionDebt,
    @TotalPay,
    @TotalPayWithBenefits,
    @Year,
    @EmployeeAgency,
    @EmployeeStatus
);";
            var insertCommand = new CommandDefinition(
                insertSql,
                parameters: new
                {
                    employee?.EmployeeName,
                    employee?.JobTitle,
                    employee?.BasePay,
                    employee?.Benefits,
                    employee?.OtherPay,
                    employee?.OvertimePay,
                    employee?.PensionDebt,
                    employee?.TotalPay,
                    employee?.TotalPayWithBenefits,
                    employee?.Year,
                    employee?.EmployeeAgency,
                    employee?.EmployeeStatus
                },
                transaction: _dbTransaction,
                cancellationToken: cancellationToken);

            return await _dbConnection.ExecuteAsync(insertCommand);
        }

        public Task AddEmployeesAsync(IEnumerable<Employee> employees, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}