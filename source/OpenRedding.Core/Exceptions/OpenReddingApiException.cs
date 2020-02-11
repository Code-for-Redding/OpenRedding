namespace OpenRedding.Core.Exception
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    public class OpenReddingApiException : Exception
    {
        public OpenReddingApiException(string message, HttpStatusCode statusCode)
            : base(message)
        {
            StatusCode = statusCode;
            ApiErrors = new List<OpenReddingApiError>();
        }

        public HttpStatusCode StatusCode { get; }

        public ICollection<OpenReddingApiError> ApiErrors { get; }
    }
}