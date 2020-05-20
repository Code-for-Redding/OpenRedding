namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
	using OpenRedding.Domain.Salaries.Enums;

	public class SetEmployeeStatusAction
    {
        public SetEmployeeStatusAction(EmployeeStatus status, bool loadFromApi) =>
            (Status, LoadFromApi) = (status, loadFromApi);

        public EmployeeStatus Status { get; }

        public bool LoadFromApi { get; }
    }
}
