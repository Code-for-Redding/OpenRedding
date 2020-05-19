namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
	using OpenRedding.Domain.Salaries.Enums;

	public class SetSalarySortFieldAction
    {
        public SetSalarySortFieldAction(SalarySortField field) =>
            SortField = field;

        public SalarySortField SortField { get; }
    }
}
