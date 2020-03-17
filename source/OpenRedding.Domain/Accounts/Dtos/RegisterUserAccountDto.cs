namespace OpenRedding.Domain.Accounts.Dtos
{
    public class RegisterUserAccountDto
    {
        public string? EmailAddress { get; set; }

        public string? ConfirmedEmailAddress { get; set; }

        public string? Password { get; set; }

        public string? ConfirmedPassword { get; set; }

        public string? ReasonForUse { get; set; }
    }
}
