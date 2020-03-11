namespace OpenRedding.Domain.Accounts.Dtos
{
    public class ForgotEmailDto : AuthorizedRequestDto
    {
        public string? Email { get; set; }
    }
}