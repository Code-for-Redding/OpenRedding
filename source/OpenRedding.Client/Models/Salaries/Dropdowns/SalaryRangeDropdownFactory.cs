namespace OpenRedding.Client.Models.Salaries.Dropdowns
{
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Salaries.Enums;

    public class SalaryRangeDropdownFactory : DropdownOptionFactory<SalarySearchRange>
    {
        public override OpenReddingEnumSelectViewModel<SalarySearchRange> GetDropdownOptions() =>
            new OpenReddingEnumSelectViewModel<SalarySearchRange>()
                .AddOption(SalarySearchRange.AllSalaries, "All salaries")
                .AddOption(SalarySearchRange._0To49, "$0 to $49,999")
                .AddOption(SalarySearchRange._50To100, "$50,000 to $99,999")
                .AddOption(SalarySearchRange._100To149, "$100,000 to $149,999")
                .AddOption(SalarySearchRange._150To199, "$150,000 to $199,999")
                .AddOption(SalarySearchRange._200To249, "$200,000 to $249,999")
                .AddOption(SalarySearchRange._250To299, "$250,000 to $299,999")
                .AddOption(SalarySearchRange._300AndGreater, "$300,000 and greater");
    }
}
