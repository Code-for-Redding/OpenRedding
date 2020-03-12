namespace OpenRedding.Domain.Common.Dto
{
    /// <summary>
    /// Sort options for the various view model properties for consumers to order salaries, job titles, and name.
    /// </summary>
    public enum OpenReddingSortOption
    {
        AscendingBaseSalary = 0,
        DescendingBaseSalary = 1,
        AscendingTotalSalary = 2,
        DescendingTotalSalary = 3,
        AscendingJobTitle = 4,
        DescendingJobTitle = 5,
        AscendingName = 6,
        DescendingName = 7
    }
}
