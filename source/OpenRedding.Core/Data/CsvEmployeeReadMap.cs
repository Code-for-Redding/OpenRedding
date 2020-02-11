namespace OpenRedding.Core.Data
{
    using CsvHelper.Configuration;
    using Domain.Salaries.Dtos;

    public sealed class CsvEmployeeReadMap : ClassMap<TransparentCaliforniaCsvReadEmployeeDto>
    {
        public CsvEmployeeReadMap()
        {
            Map(e => e.EmployeeName)
                .Name("Employee Name");

            Map(e => e.BasePay)
                .Name("Base Pay");

            Map(e => e.EmployeeAgency)
                .Name("Agency");

            Map(e => e.EmployeeStatus)
                .Name("Status");

            Map(e => e.JobTitle)
                .Name("Job Title");

            Map(e => e.OtherPay)
                .Name("Other Pay");

            Map(e => e.OvertimePay)
                .Name("Overtime Pay");

            Map(e => e.PensionDebt)
                .Name("Pension Debt")
                .Optional();

            Map(e => e.TotalPay)
                .Name("Total Pay");

            Map(e => e.TotalPayWithBenefits)
                .Name("Total Pay & Benefits");

            Map(e => e.Year)
                .Name("Year");

            Map(e => e.EmployeeStatus)
                .Name("Status")
                .ConvertUsing(CustomConverters.StatusConverter);

            Map(e => e.EmployeeAgency)
                .Name("Agency")
                .ConvertUsing(CustomConverters.AgencyConverter);
        }
    }
}