namespace OpenRedding.Domain.Common.Miscellaneous
{
    public class OpenReddingLink
    {
        public OpenReddingLink(string href, string rel, string method) =>
            (Href, Rel, Method) = (href, rel, method);

        public string Href { get; }

        public string Rel { get; }

        public string Method { get; }
    }
}
