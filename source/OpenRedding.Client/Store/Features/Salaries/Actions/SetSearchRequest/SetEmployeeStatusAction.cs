namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
	using OpenRedding.Domain.Salaries.Enums;

	public class SetEmployeeStatusAction
    {
        public SetEmployeeStatusAction(EmployeeStatus status) =>
            Status = status;

        public EmployeeStatus Status { get; }
    }
}
