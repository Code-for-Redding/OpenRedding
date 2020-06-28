namespace OpenRedding.Domain.Zoning.Dtos
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;
    using OpenRedding.Domain.Zoning.Enums;

    public class ArcGisZoningResponseDto
    {
        public string? Type { get; set; }

        public string? Name { get; set; }

        public object? Crs { get; set; }

        [JsonPropertyName("features")]
        public IEnumerable<Feature>? Features { get; set; }
    }

    public class Feature
    {
        public object? Type { get; set; }

        [JsonPropertyName("properties")]
        public ZoningResponseDto? Properties { get; set; }

        public object? Geometry { get; set; }
    }

    public class ZoningResponseDto
    {
        public int Fid { get; set; }

        [JsonPropertyName("ZONING")]
        public string? Zoning { get; set; }

        [JsonPropertyName("Z_CLASS")]
        public ZoningClass ZClass { get; set; }

        [JsonPropertyName("Z_DESC")]
        public string? ZDesc { get; set; }

        [JsonPropertyName("ZONE_ID")]
        public decimal ZoneId { get; set; }

        [JsonPropertyName("BASE_DIST")]
        public string? BaseDist { get; set; }

        [JsonPropertyName("OVERLAY_DIST1")]
        public string? OverlayDist1 { get; set; }

        [JsonPropertyName("OVERLAY_DIST2")]
        public string? OverlayDist2 { get; set; }

        [JsonPropertyName("Z_DESC2")]
        public string? ZDesc2 { get; set; }

        [JsonPropertyName("SHAPE_Length")]
        public double ShapeLength { get; set; }

        [JsonPropertyName("SHAPE_Area")]
        public double ShapeArea { get; set; }
    }
}
