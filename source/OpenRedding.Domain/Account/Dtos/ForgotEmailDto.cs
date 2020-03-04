namespace OpenRedding.Domain.Account.Dtos
{
    public class ForgotEmailDto : AuthorizedRequestDto
    {
        public string? Email { get; set; }
    }
}
