namespace OpenRedding.Core.Interfaces
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IAccountService
    {
        Task ChangeUserEmail(CancellationToken cancellationToken);
    }
}
