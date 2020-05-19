namespace OpenRedding.Domain.Salaries.Enums
{
    /// <summary>
    /// Sort options for the various view model properties for consumers to order salaries, job titles, and name.
    /// </summary>
    public enum SalarySortField
    {
        Default,
        Name,
        JobTitle,
        Year,
        BaseSalary,
        TotalWithBenefitsSalary
    }
}
