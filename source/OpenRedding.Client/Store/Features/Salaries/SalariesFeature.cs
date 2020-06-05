namespace OpenRedding.Client.Store.Features.Salaries
{
    using Fluxor;
    using OpenRedding.Client.Store;

    public class SalariesFeature : Feature<OpenReddingAppState>
    {
        public override string GetName() => "Salaries";

        protected override OpenReddingAppState GetInitialState() => new OpenReddingAppState(false, false, null, null, null);
    }
}
