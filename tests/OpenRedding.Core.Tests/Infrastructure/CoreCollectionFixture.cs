namespace OpenRedding.Core.Tests.Infrastructure
{
    using Xunit;

    [CollectionDefinition("CoreCollectionFixture")]
    public class CoreCollectionFixture : ICollectionFixture<CoreTestFixture>
    {
    }
}
