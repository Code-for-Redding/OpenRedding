namespace OpenRedding.Client.Store.Common
{
    using OpenRedding.Domain.Common.Dto;

    public class TypedStandardAction<TPayload> : IStandardAction
        where TPayload : class
    {
        public TypedStandardAction(string type, TPayload payload, OpenReddingError? error = null) =>
            (Type, Payload, Error) = (type, payload, error);

        public string Type { get; }

        public TPayload Payload { get; }

        public OpenReddingError? Error { get; }

        public bool HasError => Error != null;
    }
}
