namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
    using OpenRedding.Domain.Salaries.Enums;

    public class SetSalarySearchTotalRangeAction
    {
        public SetSalarySearchTotalRangeAction(SalarySearchRange range) =>
            Range = range;

        public SalarySearchRange Range { get; }
    }
}
