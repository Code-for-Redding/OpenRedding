namespace OpenRedding.Domain.Common.Dto
{
    public class OpenReddingError
    {
        public OpenReddingError(string type, string detail) =>
            (Type, Detail) = (type, detail);

        public string Type { get; set; }

        public string Detail { get; set; }
    }
}
