namespace OpenRedding.Client.Store.Features.Salaries.Actions.SetSearchRequest
{
    public class SetEmployeeNameAction
    {
        public SetEmployeeNameAction(string? name) =>
            Name = name;

        public string? Name { get; }
    }
}
