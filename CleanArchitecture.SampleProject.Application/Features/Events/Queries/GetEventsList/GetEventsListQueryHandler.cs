using CleanArchitecture.SampleProject.Application.Contracts.Common;
using CleanArchitecture.SampleProject.Application.Contracts.Persistence;
using CleanArchitecture.SampleProject.Domain.Entities;
using Mapster;

namespace CleanArchitecture.SampleProject.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventsListQueryHandler : IQueryHandler<GetEventsListQuery, List<EventListVm>>
    {
        private readonly IAsyncRepository<Event> _eventRepository;

        public GetEventsListQueryHandler(IAsyncRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<List<EventListVm>> Handle(GetEventsListQuery request, CancellationToken cancellationToken)
        {
            var allEvents = (await _eventRepository.ListAllAsync()).OrderBy(x => x.Date);
            return allEvents.Adapt<List<EventListVm>>();
        }
    }
}
