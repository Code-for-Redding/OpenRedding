namespace OpenRedding.Domain.Common.ViewModels
{
    using System.Collections.Generic;
    using Dto;

    public class OpenReddingErrorViewModel : OpenReddingViewModel
    {
        public OpenReddingErrorViewModel(string summary, IEnumerable<OpenReddingErrorDto> errors) =>
            (Summary, Errors) = (summary, errors);

        public string Summary { get; }

        public IEnumerable<OpenReddingErrorDto> Errors { get; }
    }
}