namespace OpenRedding.Domain.Accounts.ViewModels
{
    using OpenRedding.Domain.Common.ViewModels;

    public class RegisteredUserViewModel : OpenReddingViewModel
    {
        public RegisteredUserViewModel(string userId, string email) => (UserId, Email) = (userId, email);

        public string UserId { get; set; }

        public string Email { get; set; }
    }
}
