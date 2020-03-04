namespace OpenRedding.Core.Salaries.Queries.RetrieveEmployeeSalary
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Domain.Salaries.ViewModels;
    using Exception;
    using Extensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OpenRedding.Domain.Salaries.Queries;
    using Shared;

    public class RetrieveEmployeeSalaryQueryHandler : IRequestHandler<RetrieveEmployeeSalaryQuery, EmployeeSalaryDetailViewModel>
    {
        private readonly IOpenReddingDbContext _context;

        public RetrieveEmployeeSalaryQueryHandler(IOpenReddingDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeSalaryDetailViewModel> Handle(RetrieveEmployeeSalaryQuery request, CancellationToken cancellationToken)
        {
            ArgumentValidation.ValidateNotNull(request);

            var employeeSalary = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == request.Id, cancellationToken);

            if (employeeSalary is null)
            {
                throw new OpenReddingApiException($"Employee with ID {request?.Id} was not found", HttpStatusCode.NotFound);
            }

            return new EmployeeSalaryDetailViewModel(employeeSalary.ToEmployeeSalaryDetailDto());
        }
    }
}