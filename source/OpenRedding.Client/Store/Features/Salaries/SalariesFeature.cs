namespace OpenRedding.Client.Store.Features.Salaries
{
    using Fluxor;

    public class SalariesFeature : Feature<SalariesState>
    {
        public override string GetName() => "Salaries";

        protected override SalariesState GetInitialState() => new SalariesState(false, false, null, null, null);
    }
}
