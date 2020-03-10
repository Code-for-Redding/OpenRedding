namespace OpenRedding.Domain.Common.ViewModels
{
    public class OpenReddingGenericResponseViewModel : OpenReddingViewModel
    {
        public OpenReddingGenericResponseViewModel(string message) =>
            Message = message;

        public string Message { get; }
    }
}