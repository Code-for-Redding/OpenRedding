namespace OpenRedding.Core.Accounts.Commands.ConfirmEmailChange
{
    using OpenRedding.Core.Infrastructure.Requests;
    using OpenRedding.Domain.Account.Dtos;
    using OpenRedding.Domain.Common.ViewModels;

    public class ConfirmEmailChangeCommand : OpenReddingRequest<OpenReddingGenericResponseViewModel>
    {
        public ConfirmEmailChangeCommand(ConfirmEmailChangeDto requestDto) =>
            Request = requestDto;

        public ConfirmEmailChangeDto Request { get; set; }
    }
}