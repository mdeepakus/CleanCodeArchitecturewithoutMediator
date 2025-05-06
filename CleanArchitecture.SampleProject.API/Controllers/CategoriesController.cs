using Asp.Versioning;
using CleanArchitecture.SampleProject.API.Dispatchers;
using CleanArchitecture.SampleProject.Application.Contracts.Common;
using CleanArchitecture.SampleProject.Application.Exceptions;
using CleanArchitecture.SampleProject.Application.Features.Categories.Commands.CreateCateogry;
using CleanArchitecture.SampleProject.Application.Features.Categories.Queries.GetCategoriesList;
using CleanArchitecture.SampleProject.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace CleanArchitecture.SampleProject.Api.Controllers
{
    /// <summary>
    /// Clean Architecture Category Controller Class
    /// </summary>
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ApiControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;

        /// <summary>
        /// Counstructor of CategoryController Class.
        /// </summary>
        /// <param name="logger">A generic logger to perform the logging activities.</param>
        /// <param name="_queryDispatcher"></param>
        /// <param name="_commandDispatcher"></param>
        /// <exception cref="ApiException"></exception>
        public CategoriesController(ILogger<CategoriesController> logger, 
                                    IQueryDispatcher _queryDispatcher, 
                                    ICommandDispatcher _commandDispatcher)
               : base(_queryDispatcher,_commandDispatcher)
            
        {
            _logger = logger ?? throw new ApiException($"Value cannot be null for the argument {nameof(logger)}.", StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// This action method is used to retrieve list of all categories.
        /// </summary>
        /// <returns>Returns the list of all the categories available.</returns>
        [HttpGet("all", Name = "GetAllCategories")]
        [ApiVersion("1")]
        [ProducesResponseType(typeof(CategoryListVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CategoryListVm>>> GetAllCategories(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Initiated GetAllCategoriesNow method call.");
            var dtos = await _queryDispatcher.Dispatch<GetCategoriesListQuery, List<CategoryListVm>>(new GetCategoriesListQuery(), cancellationToken);
            _logger.LogInformation("Completed GetAllCategoriesNow method call.");
            return Ok(dtos);
        }

        /// <summary>
        /// This action method is used to retrieve list of categories with associated events with respective category.
        /// </summary>
        /// <param name="includeHistory">A boolean flag to indicate for including historical events in response</param>
        /// <returns>Returns the list of all the categories available with list of events.</returns>
        [HttpGet("all-with-events", Name = "GetCategoriesWithEvents")]
        [ApiVersion("1")]
        [ProducesResponseType(typeof(CategoryEventListVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<CategoryEventListVm>>> GetCategoriesWithEvents(bool includeHistory, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Initiated GetCategoriesWithEvents method call.");
            GetCategoriesListWithEventsQuery getCategoriesListWithEventsQuery = new GetCategoriesListWithEventsQuery() { IncludeHistory = includeHistory };
            var dtos = await _queryDispatcher.Dispatch<GetCategoriesListWithEventsQuery, List<CategoryEventListVm>>(getCategoriesListWithEventsQuery, cancellationToken);
            _logger.LogInformation("Completed GetCategoriesWithEvents method call.");
            return Ok(dtos);
        }

        /// <summary>
        /// This action method is used to add new category.
        /// </summary>
        /// <remarks>
        /// This method create a new category 
        /// with the name provided 
        /// </remarks>
        /// <param name="createCategoryCommand">Name for the new category.</param>
        /// <returns>A newly created category.</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>
        [HttpPost(Name = "AddCategory")]
        [ApiVersion("1")]
        [ProducesResponseType(typeof(CreateCategoryCommandResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateCategoryCommandResponse>> Create([FromBody] CreateCategoryCommand createCategoryCommand, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Initiated create category method call.");
            var response = await _commandDispatcher.Dispatch<CreateCategoryCommand, CreateCategoryCommandResponse>(createCategoryCommand, cancellationToken);
            _logger.LogInformation("Completed create category method call.");
            return Ok(response);
        }
    }
}
