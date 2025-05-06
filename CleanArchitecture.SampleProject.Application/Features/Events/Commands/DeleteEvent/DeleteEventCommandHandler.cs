using CleanArchitecture.SampleProject.Application.Contracts.Common;
using CleanArchitecture.SampleProject.Application.Contracts.Persistence;
using CleanArchitecture.SampleProject.Application.Exceptions;
using CleanArchitecture.SampleProject.Domain.Entities;

namespace CleanArchitecture.SampleProject.Application.Features.Events.Commands.DeleteEvent
{
    public class DeleteEventCommandHandler : ICommandHandler<DeleteEventCommand>
    {
        private readonly IAsyncRepository<Event> _eventRepository;

        public DeleteEventCommandHandler(IAsyncRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventToDelete = await _eventRepository.GetByIdAsync(request.EventId);

            if (eventToDelete == null)
            {
                throw new NotFoundException(nameof(Event), request.EventId);
            }

            await _eventRepository.DeleteAsync(eventToDelete);
        }
    }
}
