namespace OpenRedding.Domain.Common.Validation
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class OpenReddingValidationErrors
    {
        public OpenReddingValidationErrors()
        {
            Errors = new Collection<OpenReddingValidationError>();
        }

        public ICollection<OpenReddingValidationError> Errors { get; }

        public bool HasErrors => Errors.Any();

        public int ErrorCount => Errors.Count;
    }
}