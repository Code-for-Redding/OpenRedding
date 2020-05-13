namespace OpenRedding.Core.Tests.Salaries
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Salaries.Queries.GetEmployeeSalaries;
    using Domain.Salaries.ViewModels;
    using Infrastructure;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.Enums;
    using Shouldly;
    using Xunit;

    public class GetEmployeeSalariesQueryHandlerTest : TestFixture
    {
        [Fact]
        public async Task GivenValidRequest_WhenNameIsInQuery_FiltersByEmployeeName()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto("John", default, default, default, default, null);
            var query = new GetEmployeeSalariesQuery(searchRequest, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultViewModelList>();
            result.Count.ShouldBe(1);
            result.Employees.ShouldContain(e => e.Name == "John Smith");
            result.Employees.FirstOrDefault()?.JobTitle.ShouldBe("Software Engineer");
            result.Employees.FirstOrDefault()?.Agency.ShouldBe(nameof(EmployeeAgency.Redding));
            result.Employees.FirstOrDefault()?.Status.ShouldBe(nameof(EmployeeStatus.FullTime));
        }

        [Fact]
        public async Task GivenValidRequest_WhenJobTitleAreInQuery_FiltersByJobTitle()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(default, "Software", default, default, default, null);
            var query = new GetEmployeeSalariesQuery(searchRequest, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultViewModelList>();
            result.Count.ShouldBe(3);
            result.Employees.ShouldContain(e => e.Name == "John Smith");
            result.Employees.FirstOrDefault(e => e.Name == "John Smith")?.JobTitle.ShouldBe("Software Engineer");
            result.Employees.FirstOrDefault(e => e.Name == "John Smith")?.Agency.ShouldBe(nameof(EmployeeAgency.Redding));
            result.Employees.FirstOrDefault(e => e.Name == "John Smith")?.Status.ShouldBe(nameof(EmployeeStatus.FullTime));
            result.Employees.ShouldContain(e => e.Name == "Mary Smith");
            result.Employees.FirstOrDefault(e => e.Name == "Mary Smith")?.JobTitle.ShouldBe("Software Engineering Manager");
            result.Employees.FirstOrDefault(e => e.Name == "Mary Smith")?.Agency.ShouldBe(nameof(EmployeeAgency.Redding));
            result.Employees.FirstOrDefault(e => e.Name == "Mary Smith")?.Status.ShouldBe(nameof(EmployeeStatus.FullTime));
            result.Employees.ShouldContain(e => e.Name == "Joey Mckenzie");
            result.Employees.FirstOrDefault(e => e.Name == "Joey Mckenzie")?.JobTitle.ShouldBe("Senior Software Engineer");
            result.Employees.FirstOrDefault(e => e.Name == "Joey Mckenzie")?.Agency.ShouldBe(nameof(EmployeeAgency.ShastaCounty));
            result.Employees.FirstOrDefault(e => e.Name == "Joey Mckenzie")?.Status.ShouldBe(nameof(EmployeeStatus.PartTime));
        }

        [Fact]
        public async Task GivenValidRequest_WhenAgencyIsInQuery_FiltersByAgency()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(default, default, "Redding", default, default, null);
            var query = new GetEmployeeSalariesQuery(searchRequest, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultViewModelList>();
            result.Count.ShouldBe(2);
            result.Employees.ShouldContain(e => e.Name == "John Smith");
            result.Employees.FirstOrDefault(e => e.Name == "John Smith")?.JobTitle.ShouldBe("Software Engineer");
            result.Employees.FirstOrDefault(e => e.Name == "John Smith")?.Agency.ShouldBe(nameof(EmployeeAgency.Redding));
            result.Employees.FirstOrDefault(e => e.Name == "John Smith")?.Status.ShouldBe(nameof(EmployeeStatus.FullTime));
            result.Employees.ShouldContain(e => e.Name == "Mary Smith");
            result.Employees.FirstOrDefault(e => e.Name == "Mary Smith")?.JobTitle.ShouldBe("Software Engineering Manager");
            result.Employees.FirstOrDefault(e => e.Name == "Mary Smith")?.Agency.ShouldBe(nameof(EmployeeAgency.Redding));
            result.Employees.FirstOrDefault(e => e.Name == "Mary Smith")?.Status.ShouldBe(nameof(EmployeeStatus.FullTime));
        }

        [Fact]
        public async Task GivenValidRequest_WhenStatusIsInQuery_FiltersByStatus()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(default, default, default, "PartTime", default, null);
            var query = new GetEmployeeSalariesQuery(searchRequest, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultViewModelList>();
            result.Count.ShouldBe(2);
            result.Employees.ShouldContain(e => e.Name == "Joe Shmoe");
            result.Employees.FirstOrDefault()?.JobTitle.ShouldBe("Accountant");
            result.Employees.FirstOrDefault()?.Agency.ShouldBe(nameof(EmployeeAgency.ShastaCounty));
            result.Employees.FirstOrDefault()?.Status.ShouldBe(nameof(EmployeeStatus.PartTime));
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryDoesNotFindMatch_ReturnsEmptyResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto("This", "Employee", "Doesn't", "Exist!", default, null);
            var query = new GetEmployeeSalariesQuery(searchRequest, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultViewModelList>();
            result.Employees.ShouldBeEmpty();
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByNameAscending_ReturnsOrderedAscendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto("Joe", default, default, default, nameof(SalarySortOption.AscendingName), null);
            var query = new GetEmployeeSalariesQuery(searchRequest, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultViewModelList>();
            result.Employees.ShouldNotBeEmpty();
            result.Count.ShouldBe(2);
            result.Employees.First()?.Name.ShouldBe("Joe Shmoe");
            result.Employees.ShouldContain(e => e.Name == "Joey Mckenzie");
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByNameDescending_ReturnsOrderedDescendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto("Joe", default, default, default, nameof(SalarySortOption.DescendingName), null);
            var query = new GetEmployeeSalariesQuery(searchRequest, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultViewModelList>();
            result.Employees.ShouldNotBeEmpty();
            result.Count.ShouldBe(2);
            result.Employees.First()?.Name.ShouldBe("Joey Mckenzie");
            result.Employees.ShouldContain(e => e.Name == "Joe Shmoe");
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByJobTitleAscending_ReturnsOrderedAscendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(default, "Software", default, default, nameof(SalarySortOption.AscendingJobTitle), null);
            var query = new GetEmployeeSalariesQuery(searchRequest, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultViewModelList>();
            result.Employees.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
            result.Employees.ToList()[0].Name.ShouldBe("Joey Mckenzie"); // Job title is Senior Software Engineer
            result.Employees.ToList()[1].Name.ShouldBe("John Smith"); // Job Title is Software Engineer
            result.Employees.ToList()[2].Name.ShouldBe("Mary Smith"); // Job title is Software Engineering Manager
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByJobTitleDescending_ReturnsOrderedDescendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(default, "Software", default, default, nameof(SalarySortOption.DescendingJobTitle), null);
            var query = new GetEmployeeSalariesQuery(searchRequest, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultViewModelList>();
            result.Employees.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
            result.Employees.ToList()[2].Name.ShouldBe("Joey Mckenzie"); // Job title is Senior Software Engineer
            result.Employees.ToList()[1].Name.ShouldBe("John Smith"); // Job Title is Software Engineer
            result.Employees.ToList()[0].Name.ShouldBe("Mary Smith"); // Job title is Software Engineering Manager
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByBasePayAscending_ReturnsOrderedAscendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(default, "Software", default, default, nameof(SalarySortOption.AscendingBaseSalary), null);
            var query = new GetEmployeeSalariesQuery(searchRequest, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultViewModelList>();
            result.Employees.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
            result.Employees.ToList()[0].Name.ShouldBe("Joey Mckenzie");
            result.Employees.ToList()[1].Name.ShouldBe("John Smith");
            result.Employees.ToList()[2].Name.ShouldBe("Mary Smith");
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByBasePayDescending_ReturnsOrderedDescendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(default, "Software", default, default, nameof(SalarySortOption.DescendingBaseSalary), null);
            var query = new GetEmployeeSalariesQuery(searchRequest, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultViewModelList>();
            result.Employees.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
            result.Employees.ToList()[2].Name.ShouldBe("Joey Mckenzie");
            result.Employees.ToList()[1].Name.ShouldBe("John Smith");
            result.Employees.ToList()[0].Name.ShouldBe("Mary Smith");
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByTotalPayAscending_ReturnsOrderedAscendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(default, "Software", default, default, nameof(SalarySortOption.AscendingTotalSalary), null);
            var query = new GetEmployeeSalariesQuery(searchRequest, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultViewModelList>();
            result.Employees.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
            result.Employees.ToList()[0].Name.ShouldBe("John Smith");
            result.Employees.ToList()[1].Name.ShouldBe("Joey Mckenzie");
            result.Employees.ToList()[2].Name.ShouldBe("Mary Smith");
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByTotalPayDecending_ReturnsOrderedDescendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(default, "Software", default, default, nameof(SalarySortOption.DescendingTotalSalary), null);
            var query = new GetEmployeeSalariesQuery(searchRequest, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultViewModelList>();
            result.Employees.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
            result.Employees.ToList()[2].Name.ShouldBe("John Smith");
            result.Employees.ToList()[1].Name.ShouldBe("Joey Mckenzie");
            result.Employees.ToList()[0].Name.ShouldBe("Mary Smith");
        }
    }
}
