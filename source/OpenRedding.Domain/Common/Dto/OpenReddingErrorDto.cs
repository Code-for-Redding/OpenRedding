namespace OpenRedding.Domain.Common.Dto
{
    public class OpenReddingErrorDto
    {
        public OpenReddingErrorDto(string type, string detail) =>
            (Type, Detail) = (type, detail);

        public string Type { get; }

        public string Detail { get; }
    }
}
