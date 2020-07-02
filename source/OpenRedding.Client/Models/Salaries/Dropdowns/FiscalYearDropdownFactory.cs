namespace OpenRedding.Client.Models.Salaries.Dropdowns
{
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Salaries.Enums;

    public class FiscalYearDropdownFactory : DropdownOptionFactory<FiscalYear>
    {
        public override OpenReddingEnumSelectViewModel<FiscalYear> GetDropdownOptions() =>
            new OpenReddingEnumSelectViewModel<FiscalYear>()
                .AddOption(FiscalYear.AllYears, "All Years")
                .AddOption(FiscalYear._2019, "2019")
                .AddOption(FiscalYear._2018, "2018")
                .AddOption(FiscalYear._2017, "2017")
                .AddOption(FiscalYear._2016, "2016")
                .AddOption(FiscalYear._2015, "2015")
                .AddOption(FiscalYear._2014, "2014")
                .AddOption(FiscalYear._2013, "2013")
                .AddOption(FiscalYear._2012, "2012")
                .AddOption(FiscalYear._2011, "2011");
    }
}
