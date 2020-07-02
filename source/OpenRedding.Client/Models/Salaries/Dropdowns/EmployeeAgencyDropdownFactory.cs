namespace OpenRedding.Client.Models.Salaries.Dropdowns
{
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Salaries.Enums;

    public class EmployeeAgencyDropdownFactory : DropdownOptionFactory<EmployeeAgency>
    {
        public override OpenReddingEnumSelectViewModel<EmployeeAgency> GetDropdownOptions() =>
            new OpenReddingEnumSelectViewModel<EmployeeAgency>()
                .AddOption(EmployeeAgency.AllAgencies, "All Agencies")
                .AddOption(EmployeeAgency.Redding, "Redding")
                .AddOption(EmployeeAgency.ShastaCounty, "Shasta County")
                .AddOption(EmployeeAgency.Other, "Other");
    }
}
