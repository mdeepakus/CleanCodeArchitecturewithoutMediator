using Asp.Versioning;
using CleanArchitecture.SampleProject.Application.Contracts.Identity;
using CleanArchitecture.SampleProject.Application.Exceptions;
using CleanArchitecture.SampleProject.Application.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.SampleProject.Api.Controllers
{
    /// <summary>
    /// Clean Architecture Account Controller Class
    /// </summary>
    [Produces("application/json", "application/xml")]
    [ApiController]
    [Route("v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AccountController> _logger;
        /// <summary>
        /// Counstructor of AccountController Class.
        /// </summary>
        /// <param name="authenticationService">A Service to provide IAM operations.</param>
        /// <param name="logger">A generic logger to perform the logging activities.</param>
        public AccountController(IAuthenticationService authenticationService, ILogger<AccountController> logger)
                                    
        {
            _logger = logger ?? throw new ApiException($"Value cannot be null for the argument {nameof(logger)}.", StatusCodes.Status500InternalServerError);
            _authenticationService = authenticationService ?? throw new ApiException($"Value cannot be null for the argument {nameof(authenticationService)}.", StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// This action method is used to authenticate user
        /// </summary>
        /// <param name="request">User authentication request</param>
        /// <returns>Returns the bearer token</returns>
        [HttpPost("authenticate")]
        [ApiVersion("1")]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            _logger.LogInformation("Initiated Authenticate method call.");
            return Ok(await _authenticationService.AuthenticateAsync(request));
        }

        /// <summary>
        /// This action method is used to register a new user.
        /// </summary>
        /// <param name="request">New user request</param>
        /// <returns>Returns the user id of the newly created user</returns>
        [HttpPost("register")]
        [ApiVersion("1")]
        [ProducesResponseType(typeof(RegistrationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
        {
            _logger.LogInformation("Initiated Register method call.");
            return Ok(await _authenticationService.RegisterAsync(request));
        }
    }
}
