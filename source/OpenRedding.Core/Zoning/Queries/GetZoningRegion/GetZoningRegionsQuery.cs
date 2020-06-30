namespace OpenRedding.Core.Zoning.Queries.GetZoningRegion
{
    using MediatR;
    using OpenRedding.Domain.Zoning.Dtos;

    public class GetZoningRegionsQuery : IRequest<ZoningRegionsDto>
    {
    }
}
