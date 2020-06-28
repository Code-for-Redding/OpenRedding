namespace OpenRedding.Domain.Zoning.Entities
{
    using System;
    using OpenRedding.Domain.Zoning.Enums;

    public class ReddingZone
    {
        public int Id { get; set; }

        public string? Zoning { get; set; }

        public ZoningClass ZoningClass { get; set; }

        public string? ZoningDescription { get; set; }

        public string? ZoningDescriptionExtended { get; set; }

        public int ZoneId { get; set; }

        public string? BaseDist { get; set; }

        public string? OverlayDisctrict { get; set; }

        public string? OverlayDistrictExtended { get; set; }

        public double ShapeLength { get; set; }

        public double ShapeArea { get; set; }
    }
}
