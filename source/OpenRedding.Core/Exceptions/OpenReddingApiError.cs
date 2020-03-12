namespace OpenRedding.Core.Exception
{
    public class OpenReddingApiError
    {
        public OpenReddingApiError(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }

        public string ErrorMessage { get; }

        public string PropertyName { get; }
    }
}
