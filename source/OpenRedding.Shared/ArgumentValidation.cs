namespace OpenRedding.Shared
{
    using System;
    using System.Linq;

    public static class ArgumentValidation
    {
        public static void ValidateNotNull(params object[]? methodParameters)
        {
            if (methodParameters?.Any(parameter => parameter is null) != false)
            {
                throw new ArgumentNullException(nameof(methodParameters), "Null parameter detected when none expected");
            }
        }

        public static void CheckNotNull(object? parameter, string name)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(name, "Null parameter detected when expected to have value");
            }
        }
    }
}