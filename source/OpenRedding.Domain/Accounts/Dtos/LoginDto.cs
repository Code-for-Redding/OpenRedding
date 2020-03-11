namespace OpenRedding.Domain.Accounts.Dtos
{
    public class LoginDto
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public bool RememberLogin { get; set; }

        public string? ReturnUrl { get; set; }
    }
}