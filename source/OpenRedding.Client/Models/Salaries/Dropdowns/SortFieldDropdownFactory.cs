namespace OpenRedding.Client.Models.Salaries.Dropdowns
{
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Salaries.Enums;

    public class SortFieldDropdownFactory : DropdownOptionFactory<SalarySortField>
    {
        public override OpenReddingEnumSelectViewModel<SalarySortField> GetDropdownOptions() =>
            new OpenReddingEnumSelectViewModel<SalarySortField>()
                .AddOption(SalarySortField.Default, "Default")
                .AddOption(SalarySortField.Name, "Name")
                .AddOption(SalarySortField.JobTitle, "Job Title")
                .AddOption(SalarySortField.Year, "Year")
                .AddOption(SalarySortField.BaseSalary, "Base Pay")
                .AddOption(SalarySortField.TotalWithBenefitsSalary, "Total Pay and Benefits");
    }
}
