namespace OpenRedding.Domain.Common.Validation
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class OpenReddingValidationErrors
    {
        public OpenReddingValidationErrors()
        {
            Errors = new Collection<OpenReddingValidationError>();
        }

        public ICollection<OpenReddingValidationError> Errors { get; }

        public bool HasErrors => Errors.Count > 0;

        public int ErrorCount => Errors.Count;
    }
}
