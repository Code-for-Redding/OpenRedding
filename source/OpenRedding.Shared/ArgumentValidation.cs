namespace OpenRedding.Shared
{
    using System;
    using System.Linq;

    public static class ArgumentValidation
    {
#pragma warning disable SA1011 // Closing square brackets should be spaced correctly

        public static void ValidateNotNull(params object[]? methodParameters)
#pragma warning restore SA1011 // Closing square brackets should be spaced correctly
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
