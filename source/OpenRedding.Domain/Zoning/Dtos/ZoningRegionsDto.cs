namespace OpenRedding.Domain.Zoning.Dtos
{
    using System.Collections.Generic;

    public class ZoningRegionsDto
    {
        public ZoningRegionsDto(IEnumerable<string> zoningRegions) =>
            ZoningRegions = zoningRegions;

        public IEnumerable<string> ZoningRegions { get; }
    }
}
