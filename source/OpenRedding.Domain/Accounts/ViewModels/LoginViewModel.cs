﻿namespace OpenRedding.Domain.Accounts.ViewModels
{
    public class LoginViewModel : IdentityViewModel
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
