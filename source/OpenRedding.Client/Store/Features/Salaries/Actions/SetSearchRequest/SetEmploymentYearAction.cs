namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
    using OpenRedding.Domain.Salaries.Enums;

    public class SetEmploymentYearAction
    {
        public SetEmploymentYearAction(EmploymentYear year) =>
            Year = year;

        public EmploymentYear Year { get; }
    }
}
