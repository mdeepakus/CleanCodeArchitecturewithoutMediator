using CleanArchitecture.SampleProject.Application.Models.Authentication;

namespace CleanArchitecture.SampleProject.Application.Contracts.Identity
{
    /// <summary>
    /// Represents the basic functionality for user registration and authentication.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticate the user credentials.
        /// </summary>
        /// <param name="request">Authentication request entity.</param>
        /// <returns>Returns the bearer token.</returns>
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        /// <summary>
        /// Register a new user.
        /// </summary>
        /// <param name="request">New user registration request entity.</param>
        /// <returns>Returns the user id for newly registered user.</returns>
        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);
    }
}
