namespace OpenRedding.Infrastructure.Services.Seeds.Zoning
{
    using System;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using OpenRedding.Domain.Zoning.Enums;

    public class ZoningClassEnumConverter : JsonConverter<ZoningClass>
    {
        public override ZoningClass Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return ZoningClass.Unknown;
            }

            return reader.GetString() switch
            {
                "Commercial" => ZoningClass.Commercial,
                "Industry" => ZoningClass.Industry,
                "Mixed Use" => ZoningClass.MixedUse,
                "Multiple Family" => ZoningClass.MultipleFamily,
                "Office" => ZoningClass.Office,
                "Open Space" => ZoningClass.OpenSpace,
                "Public" => ZoningClass.Public,
                "Rural Lands District" => ZoningClass.RuralLandsDistrict,
                "Single Family" => ZoningClass.SingleFamily,
                "Specific Plan" => ZoningClass.SpecificPlan,
                _ => ZoningClass.Other,
            };
        }

        public override void Write(Utf8JsonWriter writer, ZoningClass value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case ZoningClass.Commercial:
                    writer.WriteStringValue(nameof(ZoningClass.Commercial));
                    return;
                case ZoningClass.Industry:
                    writer.WriteStringValue(nameof(ZoningClass.Industry));
                    return;
                case ZoningClass.MixedUse:
                    writer.WriteStringValue("Mixed Use");
                    return;
                case ZoningClass.MultipleFamily:
                    writer.WriteStringValue("Multiple Family");
                    return;
                case ZoningClass.Office:
                    writer.WriteStringValue(nameof(ZoningClass.Office));
                    return;
                case ZoningClass.OpenSpace:
                    writer.WriteStringValue("Open Space");
                    return;
                case ZoningClass.Public:
                    writer.WriteStringValue(nameof(ZoningClass.Public));
                    return;
                case ZoningClass.RuralLandsDistrict:
                    writer.WriteStringValue("Rural Lands District");
                    return;
                case ZoningClass.SingleFamily:
                    writer.WriteStringValue("Single Family");
                    return;
                case ZoningClass.SpecificPlan:
                    writer.WriteStringValue("Specific Plan");
                    return;
                default:
                    writer.WriteStringValue(nameof(ZoningClass.Other));
                    return;
            }
        }
    }
}
