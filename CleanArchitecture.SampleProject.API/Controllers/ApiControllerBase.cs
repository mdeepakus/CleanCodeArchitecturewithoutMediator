
using CleanArchitecture.SampleProject.Application.Contracts.Common;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.SampleProject.Api.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    [Produces("application/json", "application/xml")]
    [ApiController]
    [Route("v1/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly IQueryDispatcher _queryDispatcher;
        /// <summary>
        /// 
        /// </summary>
        protected readonly ICommandDispatcher _commandDispatcher;

        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="queryDispatcher"></param>
        /// <param name="commandDispatcher"></param>
        /// <exception cref="ArgumentNullException"></exception>
        protected ApiControllerBase(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher)); 
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));  
        }
    }
}