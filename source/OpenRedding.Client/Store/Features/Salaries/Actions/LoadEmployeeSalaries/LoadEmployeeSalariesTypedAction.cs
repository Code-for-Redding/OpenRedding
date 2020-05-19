namespace OpenRedding.Client.Store.Features.Salaries.Actions.LoadEmployeeSalaries
{
    using OpenRedding.Client.Store.Common;
    using OpenRedding.Domain.Common.Dto;

    public class LoadEmployeeSalariesTypedAction : TypedStandardAction<LoadEmployeeSalariesPayload>
    {
        public LoadEmployeeSalariesTypedAction(string type, LoadEmployeeSalariesPayload payload, OpenReddingError? error = null)
            : base(type, payload, error)
        {
        }
    }
}
