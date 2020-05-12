namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries
{
    using OpenRedding.Domain.Common.ViewModels;
    using OpenRedding.Domain.Salaries.Dtos;

    public class LoadEmployeeSalariesSuccessAction
    {
        public LoadEmployeeSalariesSuccessAction(OpenReddingPagedViewModel<EmployeeSalarySearchResultDto> response) =>
            Response = response;

        public OpenReddingPagedViewModel<EmployeeSalarySearchResultDto> Response { get; }
    }
}
