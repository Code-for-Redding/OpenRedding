namespace OpenRedding.Domain.Salaries.Dtos
{
    using OpenRedding.Domain.Common.Miscellaneous;

    public class RelatedEmployeeDetailDto
    {
        public RelatedEmployeeDetailDto(string name, string jobTitle, int year, OpenReddingLink self) =>
            (Name, JobTitle, Year, Self) = (name, jobTitle, year, self);

        public string Name { get; }

        public string JobTitle { get; set; }

        public int Year { get; }

        public OpenReddingLink Self { get; }
    }
}
