namespace OpenRedding.Core.Exception
{
    using System;
    using System.Collections.Generic;
    using System.Net;

#pragma warning disable RCS1194 // Implement exception constructors.

    public class OpenReddingApiException : Exception
#pragma warning restore RCS1194 // Implement exception constructors.
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
