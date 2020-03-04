namespace OpenRedding.Core.Data
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenRedding.Domain.Salaries.Entities;

    public interface ISalaryRepository
    {
        Task<int> AddEmployeeAsync(Employee employee, CancellationToken cancellationToken);

        Task AddEmployeesAsync(IEnumerable<Employee> employees, CancellationToken cancellationToken);
    }
}