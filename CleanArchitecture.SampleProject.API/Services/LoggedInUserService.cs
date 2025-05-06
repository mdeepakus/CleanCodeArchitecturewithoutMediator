using CleanArchitecture.SampleProject.Application.Contracts;
using System.Security.Claims;

namespace CleanArchitecture.SampleProject.Api.Services
{
    /// <summary>
    /// Logged In user service
    /// </summary>
    public class LoggedInUserService : ILoggedInUserService
    {
        /// <summary>
        /// Counstructor of LoggedInUserService Class.
        /// </summary>
        /// <param name="httpContextAccessor">Provides access to the current HttpContext</param>
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        /// <summary>
        /// Gets the userid for the logged in user.
        /// </summary>
        public string UserId { get; }
    }
}
