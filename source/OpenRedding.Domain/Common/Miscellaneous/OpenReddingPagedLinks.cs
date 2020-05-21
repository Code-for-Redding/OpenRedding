namespace OpenRedding.Domain.Common.Miscellaneous
{
    public class OpenReddingPagedLinks
    {
        public OpenReddingPagedLinks(
            OpenReddingLink next,
            OpenReddingLink previous,
            OpenReddingLink first,
            OpenReddingLink last,
            OpenReddingLink paged)
        {
            Next = next;
            Previous = previous;
            First = first;
            Last = last;
            Paged = paged;
        }

        public OpenReddingLink Next { get; }

        public OpenReddingLink Previous { get; }

        public OpenReddingLink First { get; }

        public OpenReddingLink Last { get; }

        public OpenReddingLink Paged { get; }
    }
}
