namespace OpenRedding.Core.Salaries.Queries.RetrieveEmployeeSalary
{
    using System;
    using OpenRedding.Core.Infrastructure.Requests;
    using OpenRedding.Domain.Salaries.ViewModels;

    public class RetrieveEmployeeSalaryQuery : OpenReddingRequest<EmployeeSalaryDetailViewModel>
    {
        public RetrieveEmployeeSalaryQuery(int id, Uri gatewayUrl) =>
            (Id, GatewayUrl) = (id, gatewayUrl);

        public int Id { get; }

        public Uri GatewayUrl { get; }
    }
}
