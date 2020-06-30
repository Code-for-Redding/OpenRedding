namespace OpenRedding.Domain.Zoning.Dtos
{
    public class ZoneSearchRequestDto
    {
        public ZoneSearchRequestDto(string? zoning, int? zoningClass) =>
            (Zoning, ZoningClass) = (zoning, zoningClass);

        public string? Zoning { get; }

        public int? ZoningClass { get; }
    }
}
