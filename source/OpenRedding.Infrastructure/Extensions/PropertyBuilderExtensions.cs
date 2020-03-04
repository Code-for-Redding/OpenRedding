namespace OpenRedding.Infrastructure.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public static class PropertyBuilderExtensions
    {
        public static void HasSixDigitTwoDecimalPlaceCurrencyType<TProperty>(this PropertyBuilder<TProperty> builder)
        {
            builder.HasColumnType("DECIMAL(9,2)");
        }
    }
}