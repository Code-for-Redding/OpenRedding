namespace OpenRedding.Domain.Common.Validation
{
    public class OpenReddingValidationError
    {
        public OpenReddingValidationError(string propertyName, string detail) =>
            (PropertyName, Detail) = (propertyName, detail);

        public string PropertyName { get; }

        public string Detail { get; }
    }
}