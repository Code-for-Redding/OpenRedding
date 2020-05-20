namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
    using OpenRedding.Domain.Salaries.Enums;

    public class SetEmployeeAgencyAction
    {
        public SetEmployeeAgencyAction(EmployeeAgency agency, bool loadFromApi) =>
            (Agency, LoadFromApi) = (agency, loadFromApi);

        public EmployeeAgency Agency { get; }

        public bool LoadFromApi { get; }
    }
}
