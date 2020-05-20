namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
    using OpenRedding.Domain.Salaries.Enums;

    public class SetSalarySortByAction
    {
        public SetSalarySortByAction(SalarySortByOption option) =>
            Option = option;

        public SalarySortByOption Option { get; set; }
    }
}
