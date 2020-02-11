namespace OpenRedding.Core.Common
{
    using Domain.Common.Validation;
    using MediatR;

    public class IOpenReddingRequest<T> : IRequest<T>
    {
        public IOpenReddingRequest()
        {
            ValidationErrors = new OpenReddingValidationErrors();
        }

        public OpenReddingValidationErrors ValidationErrors { get; set; }
    }
}