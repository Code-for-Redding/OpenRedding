namespace OpenRedding.Domain.Account.Dtos
{
    public class ResetPasswordDto : AuthorizedRequestDto
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }

        public string? Code { get; set; }
    }
}
