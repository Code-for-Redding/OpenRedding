namespace OpenRedding.Core.Tests.Salaries
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Salaries.Queries.GetEmployeeSalaries;
    using Infrastructure;
    using OpenRedding.Core.Extensions;
    using OpenRedding.Domain.Common.Aggregates;
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
            var searchRequest = new EmployeeSalarySearchRequestDto(name: "John");
            var query = new GetEmployeeSalariesQuery(searchRequest, TestUri, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Results.ShouldNotBeNull();
            result.Results.ShouldNotBeEmpty();
            result.Results.ShouldBeOfType<List<EmployeeSalarySearchResultDto>>();
            result.Results.Count().ShouldBe(1);
            result.Results.ShouldContain(e => e.Name == "John Smith");
            result.Results.FirstOrDefault()?.JobTitle.ShouldBe("Software Engineer");
            result.Results.FirstOrDefault()?.Agency.ShouldBe(nameof(EmployeeAgency.Redding));
            result.Results.FirstOrDefault()?.Status.ShouldBe(EmployeeStatus.FullTime.ToFriendlyString());
        }

        [Fact]
        public async Task GivenValidRequest_WhenJobTitleAreInQuery_FiltersByJobTitle()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(jobTitle: "Software");
            var query = new GetEmployeeSalariesQuery(searchRequest, TestUri, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Results.ShouldNotBeNull();
            result.Results.ShouldNotBeEmpty();
            result.ShouldBeOfType<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>();
            result.Count.ShouldBe(3);
            result.Results.ShouldBeOfType<List<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldContain(e => e.Name == "John Smith");
            result.Results.FirstOrDefault(e => e.Name == "John Smith")?.JobTitle.ShouldBe("Software Engineer");
            result.Results.FirstOrDefault(e => e.Name == "John Smith")?.Agency.ShouldBe(nameof(EmployeeAgency.Redding));
            result.Results.FirstOrDefault(e => e.Name == "John Smith")?.Status.ShouldBe(EmployeeStatus.FullTime.ToFriendlyString());
            result.Results.ShouldContain(e => e.Name == "Mary Smith");
            result.Results.FirstOrDefault(e => e.Name == "Mary Smith")?.JobTitle.ShouldBe("Software Engineering Manager");
            result.Results.FirstOrDefault(e => e.Name == "Mary Smith")?.Agency.ShouldBe(nameof(EmployeeAgency.Redding));
            result.Results.FirstOrDefault(e => e.Name == "Mary Smith")?.Status.ShouldBe(EmployeeStatus.FullTime.ToFriendlyString());
            result.Results.ShouldContain(e => e.Name == "Joey Mckenzie");
            result.Results.FirstOrDefault(e => e.Name == "Joey Mckenzie")?.JobTitle.ShouldBe("Senior Software Engineer");
            result.Results.FirstOrDefault(e => e.Name == "Joey Mckenzie")?.Agency.ShouldBe(EmployeeAgency.ShastaCounty.ToFriendlyString());
            result.Results.FirstOrDefault(e => e.Name == "Joey Mckenzie")?.Status.ShouldBe(EmployeeStatus.PartTime.ToFriendlyString());
        }

        [Fact]
        public async Task GivenValidRequest_WhenAgencyIsInQuery_FiltersByAgency()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(agency: nameof(EmployeeAgency.Redding));
            var query = new GetEmployeeSalariesQuery(searchRequest, TestUri, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Results.ShouldNotBeNull();
            result.Results.ShouldNotBeEmpty();
            result.ShouldBeOfType<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>();
            result.Count.ShouldBe(2);
            result.Results.ShouldBeOfType<List<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldContain(e => e.Name == "John Smith");
            result.Results.FirstOrDefault(e => e.Name == "John Smith")?.JobTitle.ShouldBe("Software Engineer");
            result.Results.FirstOrDefault(e => e.Name == "John Smith")?.Agency.ShouldBe(nameof(EmployeeAgency.Redding));
            result.Results.FirstOrDefault(e => e.Name == "John Smith")?.Status.ShouldBe(EmployeeStatus.FullTime.ToFriendlyString());
            result.Results.ShouldContain(e => e.Name == "Mary Smith");
            result.Results.FirstOrDefault(e => e.Name == "Mary Smith")?.JobTitle.ShouldBe("Software Engineering Manager");
            result.Results.FirstOrDefault(e => e.Name == "Mary Smith")?.Agency.ShouldBe(nameof(EmployeeAgency.Redding));
            result.Results.FirstOrDefault(e => e.Name == "Mary Smith")?.Status.ShouldBe(EmployeeStatus.FullTime.ToFriendlyString());
        }

        [Fact]
        public async Task GivenValidRequest_WhenStatusIsInQuery_FiltersByStatus()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(status: nameof(EmployeeStatus.PartTime));
            var query = new GetEmployeeSalariesQuery(searchRequest, TestUri, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>();
            result.Count.ShouldBe(2);
            result.Results.ShouldBeOfType<List<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldContain(e => e.Name == "Joe Schmoe");
            result.Results.FirstOrDefault()?.JobTitle.ShouldBe("Accountant");
            result.Results.FirstOrDefault()?.Agency.ShouldBe(EmployeeAgency.ShastaCounty.ToFriendlyString());
            result.Results.FirstOrDefault()?.Status.ShouldBe(EmployeeStatus.PartTime.ToFriendlyString());
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryDoesNotFindMatch_ReturnsEmptyResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(name: "This Employee Doesn't Exist!");
            var query = new GetEmployeeSalariesQuery(searchRequest, TestUri, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldBeEmpty();
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByNameAscending_ReturnsOrderedAscendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(name: "Joe", sortField: nameof(SalarySortField.Name), sortBy: nameof(SalarySortByOption.Ascending));
            var query = new GetEmployeeSalariesQuery(searchRequest, TestUri, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldBeOfType<List<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldNotBeEmpty();
            result.Count.ShouldBe(2);
            result.Results.First()?.Name.ShouldBe("Joey Mckenzie");
            result.Results.ShouldContain(e => e.Name == "Joe Schmoe");
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByNameDescending_ReturnsOrderedDescendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(name: "Joe", sortField: nameof(SalarySortField.Name), sortBy: nameof(SalarySortByOption.Descending));
            var query = new GetEmployeeSalariesQuery(searchRequest, TestUri, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldBeOfType<List<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldNotBeEmpty();
            result.Count.ShouldBe(2);
            result.Results.First()?.Name.ShouldBe("Joe Schmoe");
            result.Results.ShouldContain(e => e.Name == "Joey Mckenzie");
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByJobTitleAscending_ReturnsOrderedAscendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(jobTitle: "Software", sortField: nameof(SalarySortField.JobTitle), sortBy: nameof(SalarySortByOption.Ascending));
            var query = new GetEmployeeSalariesQuery(searchRequest, TestUri, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldBeOfType<List<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
            result.Results.ToList()[0].Name.ShouldBe("Joey Mckenzie"); // Job title is Senior Software Engineer
            result.Results.ToList()[1].Name.ShouldBe("John Smith"); // Job Title is Software Engineer
            result.Results.ToList()[2].Name.ShouldBe("Mary Smith"); // Job title is Software Engineering Manager
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByJobTitleDescending_ReturnsOrderedDescendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(jobTitle: "Software", sortField: nameof(SalarySortField.JobTitle), sortBy: nameof(SalarySortByOption.Descending));
            var query = new GetEmployeeSalariesQuery(searchRequest, TestUri, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldBeOfType<List<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
            result.Results.ToList()[2].Name.ShouldBe("Joey Mckenzie"); // Job title is Senior Software Engineer
            result.Results.ToList()[1].Name.ShouldBe("John Smith"); // Job Title is Software Engineer
            result.Results.ToList()[0].Name.ShouldBe("Mary Smith"); // Job title is Software Engineering Manager
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByBasePayAscending_ReturnsOrderedAscendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(jobTitle: "Software", sortField: nameof(SalarySortField.BaseSalary), sortBy: nameof(SalarySortByOption.Ascending));
            var query = new GetEmployeeSalariesQuery(searchRequest, TestUri, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldBeOfType<List<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
            result.Results.ToList()[0].Name.ShouldBe("Joey Mckenzie");
            result.Results.ToList()[1].Name.ShouldBe("John Smith");
            result.Results.ToList()[2].Name.ShouldBe("Mary Smith");
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByBasePayDescending_ReturnsOrderedDescendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(jobTitle: "Software", sortField: nameof(SalarySortField.BaseSalary), sortBy: nameof(SalarySortByOption.Descending));
            var query = new GetEmployeeSalariesQuery(searchRequest, TestUri, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldBeOfType<List<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
            result.Results.ToList()[2].Name.ShouldBe("Joey Mckenzie");
            result.Results.ToList()[1].Name.ShouldBe("John Smith");
            result.Results.ToList()[0].Name.ShouldBe("Mary Smith");
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByTotalPayAscending_ReturnsOrderedAscendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(jobTitle: "Software", sortField: nameof(SalarySortField.TotalWithBenefitsSalary), sortBy: nameof(SalarySortByOption.Ascending));
            var query = new GetEmployeeSalariesQuery(searchRequest, TestUri, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldBeOfType<List<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
            result.Results.ToList()[0].Name.ShouldBe("John Smith");
            result.Results.ToList()[1].Name.ShouldBe("Joey Mckenzie");
            result.Results.ToList()[2].Name.ShouldBe("Mary Smith");
        }

        [Fact]
        public async Task GivenValidRequest_WhenSearchQueryOrdersByTotalPayDecending_ReturnsOrderedDescendingResultList()
        {
            // Arrange
            var searchRequest = new EmployeeSalarySearchRequestDto(jobTitle: "Software", sortField: nameof(SalarySortField.TotalWithBenefitsSalary), sortBy: nameof(SalarySortByOption.Descending));
            var query = new GetEmployeeSalariesQuery(searchRequest, TestUri, default);
            var handler = new GetEmployeeSalariesQueryHandler(Context);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<OpenReddingSearchResultAggregate<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldBeOfType<List<EmployeeSalarySearchResultDto>>();
            result.Results.ShouldNotBeEmpty();
            result.Count.ShouldBe(3);
            result.Results.ToList()[2].Name.ShouldBe("John Smith");
            result.Results.ToList()[1].Name.ShouldBe("Joey Mckenzie");
            result.Results.ToList()[0].Name.ShouldBe("Mary Smith");
        }
    }
}
