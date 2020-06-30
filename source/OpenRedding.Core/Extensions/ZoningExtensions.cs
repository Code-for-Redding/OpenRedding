namespace OpenRedding.Core.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using OpenRedding.Domain.Common.Miscellaneous;
    using OpenRedding.Domain.Zoning.Dtos;
    using OpenRedding.Domain.Zoning.Entities;
    using OpenRedding.Domain.Zoning.Enums;
    using OpenRedding.Shared;
    using OpenRedding.Shared.Utilities;

    public static class ZoningExtensions
    {
        public static ZoningSearchResultDto ToZoningSearchResultDto(this ReddingZone zone, Uri apiUrl)
        {
            ArgumentValidation.CheckNotNull(zone, nameof(zone));
            ArgumentValidation.CheckNotNull(apiUrl, nameof(apiUrl));

            return new ZoningSearchResultDto(
                zone.Zoning.GetValueOrDefault(),
                zone.ZoningClass.ToFriendlyString(),
                zone.ZoningDescription.GetValueOrDefault(),
                zone.OverlayDisctrict.GetValueOrDefault(),
                zone.ShapeLength,
                zone.ShapeArea,
                new OpenReddingLink
                {
                    Href = apiUrl.AbsoluteUri,
                    Rel = "TODO: Add view model detail",
                    Method = HttpMethod.Get.Method
                });
        }

        public static string ToFriendlyString(this ZoningClass zoningClass)
        {
            return zoningClass switch
            {
                ZoningClass.AllZones => "All Zones",
                ZoningClass.MixedUse => "Mixed Use",
                ZoningClass.MultipleFamily => "Multiple Family",
                ZoningClass.OpenSpace => "Open Space",
                ZoningClass.RuralLandsDistrict => "Rural Lands District",
                ZoningClass.SingleFamily => "Single Family",
                ZoningClass.SpecificPlan => "Specific Plan",
                _ => zoningClass.ToString()
            };
        }
    }
}
