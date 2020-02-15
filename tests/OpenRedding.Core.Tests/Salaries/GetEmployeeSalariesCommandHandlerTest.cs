namespace OpenRedding.Core.Tests.Salaries
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Salaries.Queries.GetEmployeeSalaries;
    using Data;
    using Domain.Salaries.Entities;
    using Domain.Salaries.ViewModels;
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Shouldly;
    using Xunit;

    public class GetEmployeeSalariesCommandHandlerTest : TestFixture
    {
        [Fact]
        public async Task GivenValidRequest_WhenNameIsInQuery_FiltersByEmployeeName()
        {
            // Arrange
            var query = new GetEmployeeSalariesQuery("John", default, default, default);
            var contextMock = new Mock<IOpenReddingDbContext>();
            var handler = new GetEmployeeSalariesQueryHandler(contextMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultList>();
            contextMock.Verify(c => c.Employees.AsNoTracking(), Times.Once);
            contextMock.Verify(c => c.Employees.Where(It.IsAny<Expression<Func<Employee, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task GivenValidRequest_WhenNameAndJobTitleAreInQuery_FiltersByEmployeeName()
        {
            // Arrange
            var query = new GetEmployeeSalariesQuery("John", "Software", default, default);
            var contextMock = new Mock<IOpenReddingDbContext>();
            var handler = new GetEmployeeSalariesQueryHandler(contextMock.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<EmployeeSearchResultList>();
            contextMock.Verify(c => c.Employees.AsNoTracking(), Times.Once);
            contextMock.Verify(c => c.Employees.Where(It.IsAny<Expression<Func<Employee, bool>>>()), Times.Exactly(2));
        }
    }
}