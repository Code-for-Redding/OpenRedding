namespace OpenRedding.Infrastructure.Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using OpenRedding.Domain.Zoning.Entities;
    using OpenRedding.Domain.Zoning.Enums;
    using OpenRedding.Shared;

    public class ReddingZoneConfiguration : IEntityTypeConfiguration<ReddingZone>
    {
        public void Configure(EntityTypeBuilder<ReddingZone> builder)
        {
            ArgumentValidation.CheckNotNull(builder, nameof(builder));

            builder.Property(z => z.ZoningClass)
                .HasConversion(new EnumToStringConverter<ZoningClass>());
        }
    }
}
