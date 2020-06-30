namespace OpenRedding.Domain.Zoning.ViewModels
{
    using System;

    public class ZoningDetailViewModel
    {
        public ZoningDetailViewModel(
            string zoning,
            string zoningClass,
            string zoningDescription,
            string zoningDescriptionExtended,
            int zoneId,
            Uri baseDistrictLink,
            string overlayDisctrict,
            string overlayDistrictExtended,
            double shapeLength,
            double shapeArea)
        {
            Zoning = zoning;
            ZoningClass = zoningClass;
            ZoningDescription = zoningDescription;
            ZoningDescriptionExtended = zoningDescriptionExtended;
            ZoneId = zoneId;
            BaseDistrictLink = baseDistrictLink;
            OverlayDisctrict = overlayDisctrict;
            OverlayDistrictExtended = overlayDistrictExtended;
            ShapeLength = shapeLength;
            ShapeArea = shapeArea;
        }

        public string Zoning { get; }

        public string ZoningClass { get; }

        public string ZoningDescription { get; }

        public string ZoningDescriptionExtended { get; }

        public int ZoneId { get; }

        public Uri BaseDistrictLink { get; }

        public string OverlayDisctrict { get; }

        public string OverlayDistrictExtended { get; }

        public double ShapeLength { get; }

        public double ShapeArea { get; }
    }
}
