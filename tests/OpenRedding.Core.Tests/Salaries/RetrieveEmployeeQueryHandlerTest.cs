namespace OpenRedding.Core.Tests.Salaries
{
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using OpenRedding.Core.Exception;
    using OpenRedding.Core.Salaries.Queries.RetrieveEmployeeSalary;
    using OpenRedding.Core.Tests.Infrastructure;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.ViewModels;
    using Shouldly;
    using Xunit;

    public class RetrieveEmployeeQueryHandlerTest : TestFixture
    {
        /*
        [Fact]
        public async Task GivenValidRequest_WhenEmployeeSalaryRecordExists_ReturnsDetailViewModel()
        {
            // Arrange
            var query = new RetrieveEmployeeSalaryQuery(4);
            var handler = new RetrieveEmployeeSalaryQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSalaryDetailViewModel>();
            result.ApiVersion.ShouldNotBeNull();
            result.Employee.ShouldNotBeNull();
            result.Employee.ShouldBeOfType<EmployeeSalaryDetailDto>();
            result.Employee.Id.ShouldBe(4);
            result.Employee.JobTitle.ShouldNotBeNull();
            result.Employee.JobTitle.ShouldBe("Senior Software Engineer");
            result.Employee.Name.ShouldNotBeNull();
            result.Employee.Name.ShouldBe("Joey Mckenzie");
        }

        [Fact]
        public async Task GivenValidRequest_WhenEmployeeSalaryRecordDoesNotExist_ThrowsNotFoundOpenReddingException()
        {
            // Arrange
            var query = new RetrieveEmployeeSalaryQuery(44);
            var handler = new RetrieveEmployeeSalaryQueryHandler(Context);

            // Act
            var result = await Should.ThrowAsync<OpenReddingApiException>(async () => await handler.Handle(query, CancellationToken.None));

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OpenReddingApiException>();
            result.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        }
        */
    }
}
