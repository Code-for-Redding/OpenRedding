namespace OpenRedding.Domain.Salaries.Dtos
{
    using OpenRedding.Domain.Common.Miscellaneous;

    public class RelatedEmployeeDetailDto
    {
        public string? Name { get; set; }

        public string? JobTitle { get; set; }

        public int Year { get; set; }

        public string? Agency { get; set; }

        public decimal BasePay { get; set; }

        public decimal TotalPayWithBenefits { get; set; }

        public OpenReddingLink? Self { get; set; }
    }
}
