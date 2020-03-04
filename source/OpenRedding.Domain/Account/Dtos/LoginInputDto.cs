namespace OpenRedding.Domain.Account.Dtos
{
    public class LoginInputDto
    {
        public string? Username { get; set; }

        public string? Password { get; set; }

        public bool RememberLogin { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
