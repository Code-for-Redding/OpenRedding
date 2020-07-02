namespace OpenRedding.Client.Models.Salaries.Dropdowns
{
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Salaries.Enums;

    public class EmployeeStatusDropdownFactory : DropdownOptionFactory<EmployeeStatus>
    {
        public override OpenReddingEnumSelectViewModel<EmployeeStatus> GetDropdownOptions() =>
            new OpenReddingEnumSelectViewModel<EmployeeStatus>()
                .AddOption(EmployeeStatus.AllStatuses, "All Statuses")
                .AddOption(EmployeeStatus.FullTime, "Full-time")
                .AddOption(EmployeeStatus.PartTime, "Part-time")
                .AddOption(EmployeeStatus.Other, "Other");
    }
}
