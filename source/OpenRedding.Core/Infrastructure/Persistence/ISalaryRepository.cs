namespace OpenRedding.Core.Data
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenRedding.Domain.Salaries.Entities;

    public interface ISalaryRepository
    {
        Task<Employee?> GetEmployeeById(int id, CancellationToken cancellationToken);
    }
}
