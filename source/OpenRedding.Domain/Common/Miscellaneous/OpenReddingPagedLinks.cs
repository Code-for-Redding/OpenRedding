namespace OpenRedding.Domain.Common.Miscellaneous
{
    public class OpenReddingPagedLinks
    {
        public OpenReddingLink? Next { get; set; }

        public OpenReddingLink? Previous { get; set; }

        public OpenReddingLink? First { get; set; }

        public OpenReddingLink? Last { get; set; }

        public OpenReddingLink? Paged { get; set; }
    }
}
