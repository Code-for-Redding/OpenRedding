namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
    using OpenRedding.Domain.Salaries.Enums;

    public class SetEmployeeAgencyAction
    {
        public SetEmployeeAgencyAction(EmployeeAgency agency) =>
            Agency = agency;

        public EmployeeAgency Agency { get; }
    }
}
