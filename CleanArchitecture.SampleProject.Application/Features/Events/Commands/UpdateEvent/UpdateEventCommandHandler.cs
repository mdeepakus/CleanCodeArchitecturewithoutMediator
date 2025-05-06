using CleanArchitecture.SampleProject.Application.Contracts.Common;
using CleanArchitecture.SampleProject.Application.Contracts.Persistence;
using CleanArchitecture.SampleProject.Application.Exceptions;
using CleanArchitecture.SampleProject.Domain.Entities;
using Mapster;

namespace CleanArchitecture.SampleProject.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : ICommandHandler<UpdateEventCommand>
    {
        private readonly IAsyncRepository<Event> _eventRepository;

        public UpdateEventCommandHandler(IAsyncRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {

            var eventToUpdate = await _eventRepository.GetByIdAsync(request.EventId);

            if (eventToUpdate == null)
            {
                throw new NotFoundException(nameof(Event), request.EventId);
            }

            var validator = new UpdateEventCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            request.Adapt(eventToUpdate);

            await _eventRepository.UpdateAsync(eventToUpdate);
        }
    }
}