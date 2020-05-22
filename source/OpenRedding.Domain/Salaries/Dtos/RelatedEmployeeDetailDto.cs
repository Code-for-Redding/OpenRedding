namespace OpenRedding.Domain.Salaries.Dtos
{
    using OpenRedding.Domain.Common.Miscellaneous;

    public class RelatedEmployeeDetailDto
    {
        public string? Name { get; set; }

        public string? JobTitle { get; set; }

        public int Year { get; set; }

        public OpenReddingLink? Self { get; set; }
    }
}
