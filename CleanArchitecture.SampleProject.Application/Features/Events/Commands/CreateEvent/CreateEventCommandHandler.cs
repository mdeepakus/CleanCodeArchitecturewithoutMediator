using CleanArchitecture.SampleProject.Application.Contracts.Common;
using CleanArchitecture.SampleProject.Application.Contracts.Persistence;
using CleanArchitecture.SampleProject.Application.Contracts.Services;
using CleanArchitecture.SampleProject.Application.Exceptions;
using CleanArchitecture.SampleProject.Domain.Entities;
using Mapster;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CleanArchitecture.SampleProject.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : ICommandHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        //private readonly IEmailService _emailService;
        private readonly ILogger<CreateEventCommandHandler> _logger;
        private readonly ICreateEventService _createEventService;


        public CreateEventCommandHandler(IEventRepository eventRepository, 
                                        ILogger<CreateEventCommandHandler> logger, 
                                        ICreateEventService createEventService)
        {
            _eventRepository = eventRepository ?? throw new ApiException($"Value cannot be null for the argument {nameof(eventRepository)}.", (int)HttpStatusCode.InternalServerError);
            _logger = logger ?? throw new ApiException($"Value cannot be null for the argument {nameof(logger)}.", (int)HttpStatusCode.InternalServerError);
            _createEventService = createEventService ?? throw new ApiException($"Value cannot be null for the argument {nameof(createEventService)}.", (int)HttpStatusCode.InternalServerError);

        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateEventCommandValidator(_eventRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);

            var @event = request.Adapt<Event>();

            @event = await _createEventService.CreateEvent(@event);

            return @event.EventId;
        }
    }
}