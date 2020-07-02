namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
    using OpenRedding.Domain.Salaries.Enums;

    public class SetEmploymentYearAction
    {
        public SetEmploymentYearAction(FiscalYear year) =>
            Year = year;

        public FiscalYear Year { get; }
    }
}
