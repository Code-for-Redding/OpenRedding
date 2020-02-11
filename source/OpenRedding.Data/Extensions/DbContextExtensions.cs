// namespace OpenRedding.Data.Extensions
// {
//     using System;
//     using System.Collections.Generic;
//     using System.Threading;
//     using System.Threading.Tasks;
//     using EFCore.BulkExtensions;
//     using Microsoft.EntityFrameworkCore;
//
//     public static class DbContextExtensions
//     {
//         public static async Task BulkInsertEntity<T>(
//             this DbContext context,
//             IList<T> collectionToInsert,
//             CancellationToken cancellationToken,
//             )
//             where T : class
//         {
//             if (context is null)
//             {
//                 throw new ArgumentNullException(nameof(context), "Database context was null, please confirm the database connection");
//             }
//
//             await context.BulkInsertAsync(collectionToInsert, bulkConfig, progress, cancellationToken);
//         }
//     }
// }