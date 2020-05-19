namespace OpenRedding.Client.Store.Common
{
	using OpenRedding.Domain.Common.Dto;

    public interface IStandardAction
    {
        public string Type { get; }

        public OpenReddingError? Error { get; }

        public bool HasError { get; }
    }
}
