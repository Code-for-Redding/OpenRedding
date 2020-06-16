namespace OpenRedding.Client.Store.State
{
    public abstract class RootState
    {
        protected RootState(bool isLoading, string? errorMessage) =>
            (IsLoading, ErrorMessage) = (isLoading, errorMessage);

        public bool IsLoading { get; }

        public string? ErrorMessage { get; }

        public bool HasCurrentErrors => !string.IsNullOrWhiteSpace(ErrorMessage);
    }
}
