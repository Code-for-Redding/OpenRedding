namespace OpenRedding.Core.Tests.Extensions
{
    using System;
    using OpenRedding.Core.Extensions;
    using OpenRedding.Core.Tests.Infrastructure;
    using OpenRedding.Domain.Common.Miscellaneous;
    using OpenRedding.Domain.Salaries.Dtos;
    using OpenRedding.Domain.Salaries.Entities;
    using OpenRedding.Domain.Salaries.Enums;
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

        [Fact]
        public void ToEmployee_GivenValidCsvReadDtoWithMiddleName_ProperlyMapsToEntity()
        {
            // Arrange
            TestCsvReadDto.EmployeeName = "John P Smith";
            var tokenizedName = TestCsvReadDto.EmployeeName!.Split(' ');
            var firstName = tokenizedName[0];
            var middleName = tokenizedName[1];
            var lastName = tokenizedName[2];

            var expectedMapping = new Employee
            {
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                JobTitle = TestCsvReadDto.JobTitle,
                BasePay = TestCsvReadDto.BasePay,
                OtherPay = TestCsvReadDto.OtherPay,
                TotalPay = TestCsvReadDto.TotalPay,
                Benefits = TestCsvReadDto.Benefits,
                OvertimePay = TestCsvReadDto.OvertimePay,
                PensionDebt = TestCsvReadDto.PensionDebt,
                TotalPayWithBenefits = TestCsvReadDto.TotalPayWithBenefits,
                EmployeeAgency = EmployeeAgency.ShastaCounty,
                EmployeeStatus = EmployeeStatus.FullTime,
                Notes = TestCsvReadDto.Notes,
                Year = TestCsvReadDto.Year
            };

            // Act
            var resultMapping = TestCsvReadDto.ToEmployee();

            // Assert
            resultMapping.ShouldNotBeNull();
            resultMapping.FirstName!.ShouldBe(firstName);
            resultMapping.MiddleName!.ShouldBe(middleName);
            resultMapping.LastName!.ShouldBe(lastName);
            resultMapping.JobTitle.ShouldBe(expectedMapping.JobTitle);
            resultMapping.EmployeeAgency.ShouldBe(expectedMapping.EmployeeAgency);
            resultMapping.EmployeeStatus.ShouldBe(expectedMapping.EmployeeStatus);
            resultMapping.Year.ShouldBe(expectedMapping.Year);
            resultMapping.Notes.ShouldBe(expectedMapping.Notes);
            resultMapping.BasePay.ShouldBe(expectedMapping.BasePay);
            resultMapping.OtherPay.ShouldBe(expectedMapping.OtherPay);
            resultMapping.TotalPay.ShouldBe(expectedMapping.TotalPay);
            resultMapping.Benefits.ShouldBe(expectedMapping.Benefits);
            resultMapping.OvertimePay.ShouldBe(expectedMapping.OvertimePay);
            resultMapping.PensionDebt.ShouldBe(expectedMapping.PensionDebt);
            resultMapping.TotalPayWithBenefits.ShouldBe(expectedMapping.TotalPayWithBenefits);
        }

        [Fact]
        public void ToEmployee_GivenValidCsvReadDto_ProperlyMapsToEntity()
        {
            // Arrange
            var firstName = TestCsvReadDto.EmployeeName!.Split(' ')[0];
            var lastName = TestCsvReadDto.EmployeeName!.Split(' ')[1];
            var expectedMapping = new Employee
            {
                FirstName = firstName,
                MiddleName = string.Empty,
                LastName = lastName,
                JobTitle = TestCsvReadDto.JobTitle,
                BasePay = TestCsvReadDto.BasePay,
                OtherPay = TestCsvReadDto.OtherPay,
                TotalPay = TestCsvReadDto.TotalPay,
                Benefits = TestCsvReadDto.Benefits,
                OvertimePay = TestCsvReadDto.OvertimePay,
                PensionDebt = TestCsvReadDto.PensionDebt,
                TotalPayWithBenefits = TestCsvReadDto.TotalPayWithBenefits,
                EmployeeAgency = EmployeeAgency.ShastaCounty,
                EmployeeStatus = EmployeeStatus.FullTime,
                Notes = TestCsvReadDto.Notes,
                Year = TestCsvReadDto.Year
            };

            // Act
            var resultMapping = TestCsvReadDto.ToEmployee();

            // Assert
            resultMapping.ShouldNotBeNull();
            resultMapping.FirstName!.ShouldBe(firstName);
            resultMapping.MiddleName!.ShouldBeEmpty();
            resultMapping.LastName!.ShouldBe(lastName);
            resultMapping.JobTitle.ShouldBe(expectedMapping.JobTitle);
            resultMapping.EmployeeAgency.ShouldBe(expectedMapping.EmployeeAgency);
            resultMapping.EmployeeStatus.ShouldBe(expectedMapping.EmployeeStatus);
            resultMapping.Year.ShouldBe(expectedMapping.Year);
            resultMapping.Notes.ShouldBe(expectedMapping.Notes);
            resultMapping.BasePay.ShouldBe(expectedMapping.BasePay);
            resultMapping.OtherPay.ShouldBe(expectedMapping.OtherPay);
            resultMapping.TotalPay.ShouldBe(expectedMapping.TotalPay);
            resultMapping.Benefits.ShouldBe(expectedMapping.Benefits);
            resultMapping.OvertimePay.ShouldBe(expectedMapping.OvertimePay);
            resultMapping.PensionDebt.ShouldBe(expectedMapping.PensionDebt);
            resultMapping.TotalPayWithBenefits.ShouldBe(expectedMapping.TotalPayWithBenefits);
        }

        [Fact]
        public void ToEmployee_GivenInvalidCsvReadDto_ThrowsException()
        {
            // Arrange
            TransparentCaliforniaCsvReadEmployeeDto? nullEmployee = null;

            // Act
            var resultMapping = Should.Throw<ArgumentNullException>(() => nullEmployee!.ToEmployee());

            // Assert
            resultMapping.ShouldNotBeNull();
            resultMapping.ShouldBeOfType<ArgumentNullException>();
        }

        [Fact]
        public void ToRelatedEmployeeDetailDto_GivenValidEmployee_ProperlyMapsToDto()
        {
            // Arrange
            var expectedMapping = new RelatedEmployeeDetailDto
            {
                Id = TestEmployee.EmployeeId,
                Name = $"{TestEmployee.FirstName} {TestEmployee.MiddleName} {TestEmployee.LastName}",
                JobTitle = TestEmployee.JobTitle,
                Agency = TestEmployee.EmployeeAgency.ToFriendlyString(),
                BasePay = TestEmployee.BasePay,
                TotalPayWithBenefits = TestEmployee.TotalPayWithBenefits,
                Year = TestEmployee.Year,
                Self = new OpenReddingLink
                {
                    Href = $"{TestUri.AbsoluteUri}/salaries/{TestEmployee.EmployeeId}",
                    Rel = nameof(EmployeeSalaryDetailViewModel),
                    Method = "GET"
                }
            };

            // Act
            var resultMapping = TestEmployee.ToRelatedEmployeeDetailDto(TestUri);

            // Assert
            resultMapping.ShouldNotBeNull();
            resultMapping.Id.ShouldBe(expectedMapping.Id);
            resultMapping.Name.ShouldBe(expectedMapping.Name);
            resultMapping.JobTitle.ShouldBe(expectedMapping.JobTitle);
            resultMapping.Agency.ShouldBe(expectedMapping.Agency);
            resultMapping.BasePay.ShouldBe(expectedMapping.BasePay);
            resultMapping.Year.ShouldBe(expectedMapping.Year);
            resultMapping.TotalPayWithBenefits.ShouldBe(expectedMapping.TotalPayWithBenefits);
            resultMapping.Self.ShouldNotBeNull();
            resultMapping.Self!.Href.ShouldBe(expectedMapping.Self!.Href);
            resultMapping.Self!.Rel.ShouldBe(expectedMapping.Self!.Rel);
            resultMapping.Self!.Method.ShouldBe(expectedMapping.Self!.Method);
        }

        [Fact]
        public void ToRelatedEmployeeDetailDto_GivenInvalidEmployee_ThrowsException()
        {
            // Arrange
            Employee? nullEmployee = null;

            // Act
            var resultMapping = Should.Throw<ArgumentNullException>(() => nullEmployee!.ToRelatedEmployeeDetailDto(TestUri));

            // Assert
            resultMapping.ShouldNotBeNull();
            resultMapping.ShouldBeOfType<ArgumentNullException>();
        }

        [Fact]
        public void ToSalaryExportDto_GivenValidEmployee_ProperlyMapsToDto()
        {
            // Arrange
            var expectedMapping = new EmployeeSalaryExportDto(
                $"{TestEmployee.FirstName} {TestEmployee.MiddleName} {TestEmployee.LastName}",
                TestEmployee.JobTitle!,
                TestEmployee.BasePay,
                TestEmployee.OvertimePay,
                TestEmployee.OtherPay,
                TestEmployee.Benefits,
                TestEmployee.TotalPay,
                TestEmployee.PensionDebt!.Value,
                TestEmployee.TotalPayWithBenefits,
                TestEmployee.Year,
                TestEmployee.EmployeeAgency,
                TestEmployee.EmployeeStatus);

            // Act
            var resultMapping = TestEmployee.ToSalaryExportDto();

            // Assert
            resultMapping.ShouldNotBeNull();
            resultMapping.EmployeeName.ShouldBe(expectedMapping.EmployeeName);
            resultMapping.BasePay.ShouldBe(expectedMapping.BasePay);
            resultMapping.OvertimePay.ShouldBe(expectedMapping.OvertimePay);
            resultMapping.OtherPay.ShouldBe(expectedMapping.OtherPay);
            resultMapping.Benefits.ShouldBe(expectedMapping.Benefits);
            resultMapping.TotalPay.ShouldBe(expectedMapping.TotalPay);
            resultMapping.PensionDebt.ShouldBe(expectedMapping.PensionDebt);
            resultMapping.TotalPayWithBenefits.ShouldBe(expectedMapping.TotalPayWithBenefits);
            resultMapping.Year.ShouldBe(expectedMapping.Year);
            resultMapping.EmployeeAgency.ShouldBe(expectedMapping.EmployeeAgency);
            resultMapping.EmployeeStatus.ShouldBe(expectedMapping.EmployeeStatus);
        }

        [Fact]
        public void ToSalaryExportDto_GivenInvalidEmployee_ThrowsException()
        {
            // Arrange
            Employee? nullEmployee = null;

            // Act
            var resultMapping = Should.Throw<ArgumentNullException>(() => nullEmployee!.ToSalaryExportDto());

            // Assert
            resultMapping.ShouldNotBeNull();
            resultMapping.ShouldBeOfType<ArgumentNullException>();
        }

        [Fact]
        public void ToFriendlyString_GivenValidAgency_ReturnsStringBasedOnMatch()
        {
            // Arrange
            const string expectedShastaCounty = "Shasta County";
            const string expectedAllAgencies = "All Agencies";

            // Act
            var resultShastaCounty = EmployeeAgency.ShastaCounty.ToFriendlyString();
            var resultAllAgencies = EmployeeAgency.AllAgencies.ToFriendlyString();

            // Assert, no string splitting required for the others
            resultShastaCounty.ShouldBe(expectedShastaCounty);
            resultAllAgencies.ShouldBe(expectedAllAgencies);
            EmployeeAgency.Redding.ToFriendlyString().ShouldBe(nameof(EmployeeAgency.Redding));
            EmployeeAgency.Other.ToFriendlyString().ShouldBe(nameof(EmployeeAgency.Other));
            EmployeeAgency.Unknown.ToFriendlyString().ShouldBe(nameof(EmployeeAgency.Unknown));
        }

        [Fact]
        public void ToFriendlyString_GivenValidStatus_ReturnsStringBasedOnMatch()
        {
            // Arrange
            const string expectedFullTime = "Full-time";
            const string expectedPartTime = "Part-time";
            const string expectedAllStatuses = "All Statuses";

            // Act
            var resultFullTime = EmployeeStatus.FullTime.ToFriendlyString();
            var resultPartTime = EmployeeStatus.PartTime.ToFriendlyString();
            var resultAllStatuses = EmployeeStatus.AllStatuses.ToFriendlyString();

            // Assert, no string splitting required for the others
            resultFullTime.ShouldBe(expectedFullTime);
            resultPartTime.ShouldBe(expectedPartTime);
            resultAllStatuses.ShouldBe(expectedAllStatuses);
            EmployeeStatus.Other.ToFriendlyString().ShouldBe(nameof(EmployeeStatus.Other));
            EmployeeStatus.Unknown.ToFriendlyString().ShouldBe(nameof(EmployeeStatus.Unknown));
        }
    }
}
