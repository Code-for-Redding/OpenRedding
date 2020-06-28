namespace OpenRedding.Infrastructure.Tests.Services
{
	using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenRedding.Core.Tests.Infrastructure;
    using OpenRedding.Infrastructure.Services;
    using Xunit;

    public class SalaryCsvBlobServiceTest : InfrastructureTestFixture
    {
        [Fact]
        public async Task Test_True()
        {
            IEnumerable<object> numbers = (IEnumerable<object>)new List<int>() { 1, 2, 3 };

            var test = await TestBlobService.CreateBlobWithContents(numbers, CancellationToken.None);
        }
    }
}
