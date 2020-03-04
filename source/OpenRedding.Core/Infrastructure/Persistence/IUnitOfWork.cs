namespace OpenRedding.Core.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Salaries.Entities;

    public interface IUnitOfWork : IDisposable
    {
        ISalaryRepository SalaryRepository { get; }

        void Commit();
    }
}