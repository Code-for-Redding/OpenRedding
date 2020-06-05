namespace OpenRedding.Core.Infrastructure.Services
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Primitives;
    using OpenRedding.Domain.Common.Miscellaneous;

    public interface ILinkBuilder<out TResponse>
        where TResponse : class
    {
        public OpenReddingLink BuildLink(IEnumerable<KeyValuePair<string, StringValues>> queryCollection, string context, string httpMethod, KeyValuePair<string, string>? trailingParam = null);

        public OpenReddingPagedLinks BuildPaginationLinks(IEnumerable<KeyValuePair<string, StringValues>> queryCollection, int totalPages, int? currentPage);
    }
}
