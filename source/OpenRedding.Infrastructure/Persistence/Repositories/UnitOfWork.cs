namespace OpenRedding.Infrastructure.Persistence.Repositories
{
    using System;
    using System.Data;
    using Core.Data;
    using Microsoft.Data.SqlClient;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;
        private readonly IDbConnection _dbConnection;
        private readonly IDbTransaction _dbTransaction;

        public UnitOfWork(string connectionString)
        {
            // Open our connection and begin our transaction
            _connectionString = connectionString;
            _dbConnection = new SqlConnection(connectionString);
            _dbConnection.Open();
            _dbTransaction = _dbConnection.BeginTransaction();
            SalaryRepository = new SalaryRepository(_dbTransaction);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public ISalaryRepository SalaryRepository { get; }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not commit the transaction, reason: {e.Message}");
                _dbTransaction.Rollback();
            }
            finally
            {
                _dbTransaction.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbTransaction?.Dispose();
                _dbConnection?.Dispose();
            }
        }
    }
}