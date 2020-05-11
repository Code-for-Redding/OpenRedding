namespace OpenRedding.Core.Infrastructure.Services
{
    using System.Collections.Generic;
    using OpenRedding.Domain.Common.Miscellaneous;

    public interface ILinkBuilder<TResult>
    {
        OpenReddingPagedLinks GenerateLinks(IEnumerable<TResult> results);
    }
}
