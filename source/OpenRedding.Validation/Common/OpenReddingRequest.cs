namespace OpenRedding.Validation.Common
{
    using MediatR;
    using OpenRedding.Domain.Common.Validation;

    /// <summary>
    /// Generic implementation of the MediatR request interface used for commonality among query and command requests.
    /// </summary>
    public class OpenReddingRequest : IRequest
    {
        public OpenReddingRequest() =>
            ValidationErrors = new OpenReddingValidationErrors();

        public OpenReddingValidationErrors ValidationErrors { get; }
    }
}
