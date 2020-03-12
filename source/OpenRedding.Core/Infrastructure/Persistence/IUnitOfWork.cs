namespace OpenRedding.Core.Data
{
    using System;

    public interface IUnitOfWork : IDisposable
    {
        ISalaryRepository SalaryRepository { get; }

        void Commit();
    }
}
