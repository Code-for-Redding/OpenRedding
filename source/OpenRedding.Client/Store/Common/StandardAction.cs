namespace OpenRedding.Client.Store.Common
{
    using OpenRedding.Domain.Common.Dto;

    public class StandardAction : IStandardAction
    {
        public StandardAction(string type, OpenReddingError? error = null) =>
            (Type, Error) = (type, error);

        public string Type { get; }

        public OpenReddingError? Error { get; }

        public bool HasError => Error != null;
    }
}
