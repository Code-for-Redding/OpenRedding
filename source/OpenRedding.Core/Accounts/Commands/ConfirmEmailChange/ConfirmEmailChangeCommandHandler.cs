namespace OpenRedding.Core.Accounts.Commands.ConfirmEmailChange
{
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using OpenRedding.Domain.Common.ViewModels;

    public class ConfirmEmailChangeCommandHandler : IRequestHandler<ConfirmEmailChangeCommand, OpenReddingGenericResponseViewModel>
    {
        public Task<OpenReddingGenericResponseViewModel> Handle(ConfirmEmailChangeCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
