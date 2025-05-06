using Asp.Versioning;
using CleanArchitecture.SampleProject.Application.Contracts.Common;
using CleanArchitecture.SampleProject.Application.Exceptions;
using CleanArchitecture.SampleProject.Application.Features.Orders.GetOrdersForMonth;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.SampleProject.Api.Controllers
{
    /// <summary>
    /// Clean Architecture Order Controller Class
    /// </summary>
    public class OrderController : ApiControllerBase
    {
        private readonly ILogger<OrderController> _logger;
         
        /// <summary>Counstructor of OrderController Class.</summary>
        /// <param name="_queryDispatcher"></param>
        /// <param name="_commandDispatcher"></param>
        /// <param name="logger">A generic logger to perform the logging activities.</param>
        /// <exception cref="ApiException"></exception>
        public OrderController(IQueryDispatcher _queryDispatcher,
                                ICommandDispatcher _commandDispatcher, 
                                ILogger<OrderController> logger)
            : base(_queryDispatcher, _commandDispatcher)
        {
            _logger = logger ?? throw new ApiException($"Value cannot be null for the argument {nameof(logger)}.", StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// This action method is used to retrieve orders of the month
        /// </summary>
        /// <param name="date">To define the month and year for the list of orders.</param>
        /// <param name="page">To retrieve number of page required.</param>
        /// <param name="size">To define the nuumber of records per page.</param>
        /// <returns></returns>
        [HttpGet("/paged-orders-for-month", Name = "GetPagedOrdersForMonth")]
        [ApiVersion("1")]
        [ProducesResponseType(typeof(PagedOrdersForMonthVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedOrdersForMonthVm>> GetPagedOrdersForMonth(DateTime date, int page, int size, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Initiated GetPagedOrdersForMonth method call.");
            var getOrdersForMonthQuery = new GetOrdersForMonthQuery() { Date = date, Page = page, Size = size };
            var dtos = await _queryDispatcher.Dispatch< GetOrdersForMonthQuery, PagedOrdersForMonthVm >(getOrdersForMonthQuery, cancellationToken);
            _logger.LogInformation("Completed GetPagedOrdersForMonth method call.");

            return Ok(dtos);
        }
    }
}
