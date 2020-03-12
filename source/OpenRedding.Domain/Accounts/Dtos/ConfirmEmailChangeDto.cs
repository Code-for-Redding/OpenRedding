namespace OpenRedding.Domain.Accounts.Dtos
{
    public class ConfirmEmailChangeDto : AuthorizedRequestDto
    {
        public string? Email { get; set; }

        public string? Code { get; set; }
    }
}
