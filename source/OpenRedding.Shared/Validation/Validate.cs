namespace OpenRedding.Shared.Validation
{
    using System;

    public static class Validate
    {
        public static void NotNull(object objectToValidate, string messageOnFailedValidation)
        {
            if (objectToValidate is null)
            {
                throw new ArgumentNullException(messageOnFailedValidation);
            }
        }
    }
}
