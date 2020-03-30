namespace OpenRedding.Client.Services
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components.Authorization;

    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity(
            	new[]
                {
                    new Claim(ClaimTypes.Name, "Joey Mckenzie"),
                    new Claim(ClaimTypes.Email, "joey.mckenzie27@gmail.com"),
                }, "Test auth");

            var user = new ClaimsPrincipal(identity);

            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
