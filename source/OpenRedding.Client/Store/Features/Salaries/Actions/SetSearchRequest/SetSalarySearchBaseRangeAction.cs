namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
	using OpenRedding.Domain.Salaries.Enums;

    public class SetSalarySearchBaseRangeAction
    {
        public SetSalarySearchBaseRangeAction(SalarySearchRange range) =>
            Range = range;

        public SalarySearchRange Range { get; }
    }
}
