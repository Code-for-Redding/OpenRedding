namespace OpenRedding.Core.Tests.Extensions
{
    using System;
    using OpenRedding.Core.Extensions;
    using OpenRedding.Core.Tests.Infrastructure;
    using OpenRedding.Domain.Common.Miscellaneous;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.Entities;
    using OpenRedding.Domain.Salaries.ViewModels;
    using Shouldly;
    using Xunit;

    public class EmployeeExtensionsTest : TestFixture
    {
        [Fact]
        public void ToEmployeeSalarySearchResultDto_GivenValidEmployee_ProperlyMapsToDto()
        {
            // Arrange
            var expectedMapping = new EmployeeSalarySearchResultDto(
                TestEmployee.EmployeeId,
                $"{TestEmployee.FirstName} {TestEmployee.MiddleName} {TestEmployee.LastName}",
                TestEmployee.JobTitle,
                TestEmployee.EmployeeAgency.ToFriendlyString(),
                TestEmployee.EmployeeStatus.ToFriendlyString(),
                TestEmployee.Year,
                TestEmployee.BasePay,
                TestEmployee.TotalPayWithBenefits,
                new OpenReddingLink
                {
                    Href = $"{TestUri.AbsoluteUri}/salaries/{TestEmployee.EmployeeId}",
                    Rel = nameof(EmployeeSalaryDetailViewModel),
                    Method = "GET"
                });

            // Act
            var resultMapping = TestEmployee.ToEmployeeSalarySearchResultDto(TestUri);

            // Assert
            resultMapping.ShouldNotBeNull();
            resultMapping.Id.ShouldBe(expectedMapping.Id);
            resultMapping.Name.ShouldBe(expectedMapping.Name);
            resultMapping.JobTitle.ShouldBe(expectedMapping.JobTitle);
            resultMapping.Agency.ShouldBe(expectedMapping.Agency);
            resultMapping.Status.ShouldBe(expectedMapping.Status);
            resultMapping.Year.ShouldBe(expectedMapping.Year);
            resultMapping.BasePay.ShouldBe(expectedMapping.BasePay);
            resultMapping.TotalPayWithBenefits.ShouldBe(expectedMapping.TotalPayWithBenefits);
            resultMapping.EmployeeDetailLink.ShouldNotBeNull();
            resultMapping.EmployeeDetailLink!.Href.ShouldBe(expectedMapping.EmployeeDetailLink!.Href);
            resultMapping.EmployeeDetailLink!.Rel.ShouldBe(expectedMapping.EmployeeDetailLink!.Rel);
            resultMapping.EmployeeDetailLink!.Method.ShouldBe(expectedMapping.EmployeeDetailLink!.Method);
        }

        [Fact]
        public void ToEmployeeSalarySearchResultDto_GivenNullEmployee_ThrowsException()
        {
            // Arrange
            Employee? nullEmployee = null;

            // Act
            var resultMapping = Should.Throw<ArgumentNullException>(() => nullEmployee!.ToEmployeeSalarySearchResultDto(TestUri));

            // Assert
            resultMapping.ShouldNotBeNull();
            resultMapping.ShouldBeOfType<ArgumentNullException>();
        }

        [Fact]
        public void ToEmployeeSalaryDetailDto_GivenValidEmployee_ProperlyMapsToDto()
        {
            // Arrange
            var expectedMapping = new EmployeeSalaryDetailDto
            {
                Id = TestEmployee.EmployeeId,
                Name = $"{TestEmployee.FirstName} {TestEmployee.MiddleName} {TestEmployee.LastName}",
                JobTitle = TestEmployee.JobTitle,
                BasePay = TestEmployee.BasePay,
                Benefits = TestEmployee.Benefits,
                OtherPay = TestEmployee.OtherPay,
                OvertimePay = TestEmployee.OvertimePay,
                TotalPay = TestEmployee.TotalPay,
                TotalPayWithBenefits = TestEmployee.TotalPayWithBenefits,
                Year = TestEmployee.Year,
                Agency = TestEmployee.EmployeeAgency.ToFriendlyString(),
                Status = TestEmployee.EmployeeStatus.ToFriendlyString(),
                Self = new OpenReddingLink
                {
                    Href = $"{TestUri.AbsoluteUri}/salaries/{TestEmployee.EmployeeId}",
                    Rel = nameof(EmployeeSalaryDetailViewModel),
                    Method = "GET"
                }
            };

            // Act
            var resultMapping = TestEmployee.ToEmployeeSalaryDetailDto(TestUri);

            // Assert
            resultMapping.ShouldNotBeNull();
            resultMapping.Id.ShouldBe(expectedMapping.Id);
            resultMapping.Name.ShouldBe(expectedMapping.Name);
            resultMapping.JobTitle.ShouldBe(expectedMapping.JobTitle);
            resultMapping.Agency.ShouldBe(expectedMapping.Agency);
            resultMapping.Status.ShouldBe(expectedMapping.Status);
            resultMapping.Year.ShouldBe(expectedMapping.Year);
            resultMapping.BasePay.ShouldBe(expectedMapping.BasePay);
            resultMapping.Benefits.ShouldBe(expectedMapping.Benefits);
            resultMapping.OtherPay.ShouldBe(expectedMapping.OtherPay);
            resultMapping.OvertimePay.ShouldBe(expectedMapping.OvertimePay);
            resultMapping.TotalPay.ShouldBe(expectedMapping.TotalPay);
            resultMapping.TotalPayWithBenefits.ShouldBe(expectedMapping.TotalPayWithBenefits);
            resultMapping.Self.ShouldNotBeNull();
            resultMapping.Self!.Href.ShouldBe(expectedMapping.Self!.Href);
            resultMapping.Self!.Rel.ShouldBe(expectedMapping.Self!.Rel);
            resultMapping.Self!.Method.ShouldBe(expectedMapping.Self!.Method);
        }

        [Fact]
        public void ToEmployeeSalaryDetailDto_GivenNullEmployee_ThrowsException()
        {
            // Arrange
            Employee? nullEmployee = null;

            // Act
            var resultMapping = Should.Throw<ArgumentNullException>(() => nullEmployee!.ToEmployeeSalaryDetailDto(TestUri));

            // Assert
            resultMapping.ShouldNotBeNull();
            resultMapping.ShouldBeOfType<ArgumentNullException>();
        }
    }
}
