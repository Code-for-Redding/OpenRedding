namespace OpenRedding.Core.Infrastructure.Services
{
    using System.Collections.Generic;
    using Microsoft.Extensions.Primitives;
    using OpenRedding.Domain.Common.Miscellaneous;

    public interface ILinkBuilder
    {
        public OpenReddingLink BuildLink();

        public OpenReddingPagedLinks BuildPaginationLinks<TResponse>(IEnumerable<KeyValuePair<string, StringValues>> queryCollection, int totalPages, int? currentPage);
    }
}
