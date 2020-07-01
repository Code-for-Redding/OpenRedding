namespace OpenRedding.Shared.Utilities
{
    public static class StringExtensions
    {
        public static string GetValueOrDefault(this string? nullableStringValue, string? defaultValue = null)
        {
            if (nullableStringValue is null)
            {
                return defaultValue ?? string.Empty;
            }

            return nullableStringValue;
        }
    }
}
