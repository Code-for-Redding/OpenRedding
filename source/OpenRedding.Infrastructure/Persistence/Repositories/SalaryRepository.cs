namespace OpenRedding.Infrastructure.Persistence.Repositories
{
    using System;
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

        public Task<int> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken)
        {
            const string insertSql = @"
INSERT INTO [dbo].[Employees] (
    [EmployeeName],
    [JobTitle],
    [BasePay],
    [Benefits],
    [OtherPay],
    [OvertimePay],
    [PensionDebt],
    [TotalPay],
    [TotalPayWithBenefits],
    [Year],
    [EmployeeAgency],
    [EmployeeStatus]
) VALUES (
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
                    employee?.FirstName,
                    employee?.LastName,
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

            return _dbConnection.ExecuteAsync(insertCommand);
        }

        public Task<Employee?> GetEmployeeById(int id, CancellationToken cancellationToken)
        {
            const string employeeRetrievalSql = @"
SELECT
    *
FROM [dbo].[Employees]
WHERE [EmployeeId] = @id;
";

            var retrievalCommand = new CommandDefinition(
                employeeRetrievalSql,
                parameters: new { id },
                transaction: _dbTransaction,
                cancellationToken: cancellationToken);

            return _dbConnection.QueryFirstOrDefaultAsync<Employee?>(retrievalCommand);
        }
    }
}
