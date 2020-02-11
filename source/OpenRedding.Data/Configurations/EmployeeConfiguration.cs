namespace OpenRedding.Data.Configurations
{
    using System;
    using Domain.Salaries.Entities;
    using Extensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder), "EF entity builder for Employee is null");
            }

            builder.Property(e => e.EmployeeAgency)
                .HasConversion(new EnumToStringConverter<EmployeeAgency>());

            builder.Property(e => e.EmployeeStatus)
                .HasConversion(new EnumToStringConverter<EmployeeStatus>());

            builder.Property(e => e.BasePay)
                .HasSixDigitTwoDecimalPlaceCurrencyType();

            builder.Property(e => e.Benefits)
                .HasSixDigitTwoDecimalPlaceCurrencyType();

            builder.Property(e => e.OtherPay)
                .HasSixDigitTwoDecimalPlaceCurrencyType();

            builder.Property(e => e.OvertimePay)
                .HasSixDigitTwoDecimalPlaceCurrencyType();

            builder.Property(e => e.PensionDebt)
                .HasSixDigitTwoDecimalPlaceCurrencyType();

            builder.Property(e => e.TotalPay)
                .HasSixDigitTwoDecimalPlaceCurrencyType();

            builder.Property(e => e.TotalPayWithBenefits)
                .HasSixDigitTwoDecimalPlaceCurrencyType();
        }
    }
}