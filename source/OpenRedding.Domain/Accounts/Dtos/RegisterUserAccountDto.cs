namespace OpenRedding.Domain.Accounts.Dtos
{
    public class RegisterUserAccountDto
    {
        public RegisterUserAccountDto(string emailAddress, string confirmedEmailAddress, string password, string confirmedPassword, string reasonForUse)
        {
            EmailAddress = emailAddress;
            ConfirmedEmailAddress = confirmedEmailAddress;
            Password = password;
            ConfirmedPassword = confirmedPassword;
            ReasonForUse = reasonForUse;
        }

        public string EmailAddress { get; set; }

        public string ConfirmedEmailAddress { get; set; }

        public string Password { get; set; }

        public string ConfirmedPassword { get; set; }

        public string ReasonForUse { get; set; }
    }
}
