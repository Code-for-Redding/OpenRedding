namespace OpenRedding.Core.Salaries.Queries.GetEmployeeSalaries
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Data;
    using Extensions;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OpenRedding.Domain.Common.Aggregates;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Shared;

    public class GetEmployeeSalariesQueryHandler : IRequestHandler<GetEmployeeSalariesQuery, OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>
    {
        private readonly IOpenReddingDbContext _context;

        public GetEmployeeSalariesQueryHandler(IOpenReddingDbContext context) =>
            _context = context;

        public async Task<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>> Handle(GetEmployeeSalariesQuery request, CancellationToken cancellationToken)
        {
            ArgumentValidation.CheckNotNull(request, nameof(request));
            ArgumentValidation.CheckNotNull(request.SearchRequest, nameof(request.SearchRequest));

            // Request context to not track entity since we're not making any updates
            var queriedSalaries = _context.Employees
                .AsNoTracking()
                .FromSearchRequest(request.SearchRequest);

            // Perform the query and map each resulting record to its search DTO
            var totalResults = queriedSalaries.Count();
            var resultingSalaries = await queriedSalaries
                .Select(e => e.ToEmployeeSalarySearchResultDto(request.ApiBaseUrl))
                .SkipAndTakeDefault(request.Page)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return new OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>(resultingSalaries.AsEnumerable(), totalResults, request.Page);
        }
    }
}
