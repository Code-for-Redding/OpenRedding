namespace OpenRedding.Domain.Zoning.Dtos
{
    using OpenRedding.Domain.Common.Miscellaneous;

    public class ZoningSearchResultDto
    {
        public ZoningSearchResultDto(
            string zoning,
            string zoningClass,
            string zoningDescription,
            string overlayDisctrict,
            double shapeLength,
            double shapeArea,
            OpenReddingLink self)
        {
            Zoning = zoning;
            ZoningClass = zoningClass;
            ZoningDescription = zoningDescription;
            OverlayDisctrict = overlayDisctrict;
            ShapeLength = shapeLength;
            ShapeArea = shapeArea;
            Self = self;
        }

        public string Zoning { get; }

        public string ZoningClass { get; }

        public string ZoningDescription { get; }

        public string OverlayDisctrict { get; }

        public double ShapeLength { get; }

        public double ShapeArea { get; }

        public OpenReddingLink Self { get; }
    }
}
