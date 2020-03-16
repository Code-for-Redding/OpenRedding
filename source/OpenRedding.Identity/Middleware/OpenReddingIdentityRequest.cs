namespace OpenRedding.Identity.Middleware
{
    using MediatR;
    using OpenRedding.Domain.Common.Validation;

    /// <summary>
    /// Generic implementation of the MediatR request interface used for commonality among query and command requests.
    /// </summary>
    /// <typeparam name="T">Type of the query or command.</typeparam>
    public class OpenReddingIdentityRequest<T> : IRequest<T>
    {
        public OpenReddingIdentityRequest() =>
            ValidationErrors = new OpenReddingValidationErrors();

        public OpenReddingValidationErrors ValidationErrors { get; set; }
    }
}
