namespace OpenRedding.Core.Tests.Salaries
{
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Salaries.Queries.GetEmployeeSalaries;
    using Domain.Salaries.ViewModels;
    using Infrastructure;
    using Shouldly;
    using Xunit;

    public class GetEmployeeSalariesCommandHandlerTest : TestFixture
    {
        [Fact]
        public async Task GivenValidRequest_WhenNameIsInQuery_FiltersByEmployeeName()
        {
            // Arrange
            var query = new GetEmployeeSalariesQuery("John", default, default, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultList>();
            result.Count.ShouldBe(1);
            result.Employees.ShouldContain(e => e.Name == "John Smith");
        }

        [Fact]
        public async Task GivenValidRequest_WhenJobTitleAreInQuery_FiltersByJobTitle()
        {
            // Arrange
            var query = new GetEmployeeSalariesQuery(default, "Software", default, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultList>();
            result.Count.ShouldBe(2);
            result.Employees.ShouldContain(e => e.Name == "John Smith");
            result.Employees.ShouldContain(e => e.Name == "Mary Smith");
        }

        [Fact]
        public async Task GivenValidRequest_WhenAgencyIsInQuery_FiltersByAgency()
        {
            // Arrange
            var query = new GetEmployeeSalariesQuery(default, default, "Redding", default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultList>();
            result.Count.ShouldBe(2);
            result.Employees.ShouldContain(e => e.Name == "John Smith");
            result.Employees.ShouldContain(e => e.Name == "Mary Smith");
        }

        [Fact]
        public async Task GivenValidRequest_WhenStatusIsInQuery_FiltersByStatus()
        {
            // Arrange
            var query = new GetEmployeeSalariesQuery(default, default, default, "PartTime");
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultList>();
            result.Count.ShouldBe(1);
            result.Employees.ShouldContain(e => e.Name == "Joe Shmoe");
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryDoesNotFindMatch_ReturnsEmppyResultList()
        {
            // Arrange
            var query = new GetEmployeeSalariesQuery("This", "Employee", "Doesn't", "Exist!");
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultList>();
            result.Employees.ShouldBeEmpty();
        }
    }
}