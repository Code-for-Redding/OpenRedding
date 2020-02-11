namespace OpenRedding.Shared
{
    using System;
    using System.Linq;

    public static class ArgumentValidation
    {
        public static void ValidateNotNull(params object[]? methodParameters)
        {
            if (methodParameters is null || methodParameters.Any(parameter => parameter is null))
            {
                throw new ArgumentNullException(nameof(methodParameters), "Null parameter detected when none expected");
            }
        }
    }
}