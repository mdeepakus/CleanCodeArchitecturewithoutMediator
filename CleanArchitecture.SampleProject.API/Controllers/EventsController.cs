using Asp.Versioning;
using CleanArchitecture.SampleProject.Api.Utility;
using CleanArchitecture.SampleProject.Application.Contracts.Common;
using CleanArchitecture.SampleProject.Application.Exceptions;
using CleanArchitecture.SampleProject.Application.Features.Events.Commands.CreateEvent;
using CleanArchitecture.SampleProject.Application.Features.Events.Commands.DeleteEvent;
using CleanArchitecture.SampleProject.Application.Features.Events.Commands.UpdateEvent;
using CleanArchitecture.SampleProject.Application.Features.Events.Queries.GetEventDetail;
using CleanArchitecture.SampleProject.Application.Features.Events.Queries.GetEventsExport;
using CleanArchitecture.SampleProject.Application.Features.Events.Queries.GetEventsList;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.SampleProject.Api.Controllers
{
    /// <summary>
    /// Clean Architecture Event Controller Class
    /// </summary>
    public class EventsController : ApiControllerBase
    {
        private readonly ILogger<EventsController> _logger;

        ///// <summary>
        ///// Counstructor of EventsController Class.
        ///// </summary>
        ///// <param name="logger">A generic logger to perform the logging activities.</param>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_queryDispatcher"></param>
        /// <param name="_commandDispatcher"></param>
        /// <param name="logger"></param>
        /// <exception cref="ApiException"></exception>
        public EventsController(IQueryDispatcher _queryDispatcher,
                                ICommandDispatcher _commandDispatcher, 
                                ILogger<EventsController> logger)
            : base(_queryDispatcher, _commandDispatcher)
        {
            _logger = logger ?? throw new ApiException($"Value cannot be null for the argument {nameof(logger)}.", StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// This action method is used to retrieve list of all events. 
        /// </summary>
        /// <returns>Returns list of all the events.</returns>
        [HttpGet(Name = "GetAllEvents")]
        [ApiVersion("1")]
        [ProducesResponseType(typeof(EventListVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<EventListVm>>> GetAllEvents(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Initiated GetAllEvents method call.");
            var dtos = await _queryDispatcher.Dispatch<GetEventsListQuery, List<EventListVm>>(new GetEventsListQuery(), cancellationToken);
            _logger.LogInformation("Completed GetAllEvents method call.");

            return Ok(dtos);
        }

        /// <summary>
        /// This action method is used to retrieve details of a event for the given id.
        /// </summary>
        /// <param name="id">Event id to retrieve the event.</param>
        /// <returns>Returns the event.</returns>
        [HttpGet("{id}", Name = "GetEventById")]
        [ApiVersion("1")]
        [ProducesResponseType(typeof(EventDetailVm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EventDetailVm>> GetEventById(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Initiated GetEventById method call.");
            var getEventDetailQuery = new GetEventDetailQuery() { Id = id };

            var eventDetail = await _queryDispatcher.Dispatch<GetEventDetailQuery, EventDetailVm>(getEventDetailQuery, cancellationToken);
            _logger.LogInformation("Completed GetEventById method call.");

            return Ok(eventDetail);
        }

        /// <summary>
        /// This action method is used to add new event.
        /// </summary>
        /// <param name="createEventCommand">The request to create a event.</param>
        /// <returns>Returns the id for the newly added event.</returns>
        [HttpPost(Name = "AddEvent")]
        [ApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateEventCommand createEventCommand, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Initiated AddEvent method call.");
            var id = await _commandDispatcher.Dispatch<CreateEventCommand, Guid>(createEventCommand, cancellationToken);
            _logger.LogInformation("Completed AddEvent method call.");
            return Ok(id);
        }

        /// <summary>
        /// This action method is used to update the existing event.
        /// </summary>
        /// <param name="updateEventCommand">The request to update the event.</param>
        /// <returns>ActionResult</returns>
        [HttpPut(Name = "UpdateEvent")]
        [ApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update([FromBody] UpdateEventCommand updateEventCommand, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Initiated UpdateEvent method call.");
            await _commandDispatcher.Dispatch<UpdateEventCommand>(updateEventCommand, cancellationToken);
            _logger.LogInformation("Completed UpdateEvent method call.");
            return NoContent();
        }

        /// <summary>
        /// This action method is used to delete an existing event.
        /// </summary>
        /// <param name="id">Unique id for the event to delete.</param>
        /// <returns>ActionResult</returns>
        [HttpDelete("{id}", Name = "DeleteEvent")]
        [ApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Initiated DeleteEvent method call.");
            var deleteEventCommand = new DeleteEventCommand() { EventId = id };
            await _commandDispatcher.Dispatch<DeleteEventCommand>(deleteEventCommand, cancellationToken);
            _logger.LogInformation("Completed DeleteEvent method call.");
            return NoContent();
        }

        /// <summary>
        /// This action method is used to retrieve the list of events in text\csv format.
        /// </summary>
        /// <returns>FileResult</returns>
        [HttpGet("export", Name = "ExportEvents")]
        [ApiVersion("1")]
        [FileResultContentType("text/csv")]
        public async Task<FileResult> ExportEvents(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Initiated ExportEvents method call.");
            var fileDto = await _queryDispatcher.Dispatch<GetEventsExportQuery, EventExportFileVm>(new GetEventsExportQuery(), cancellationToken);
            _logger.LogInformation("Completed ExportEvents method call.");
            return File(fileDto.Data, fileDto.ContentType, fileDto.EventExportFileName);
        }
    }
}
