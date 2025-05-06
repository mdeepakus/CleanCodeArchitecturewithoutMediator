using CleanArchitecture.SampleProject.Application.Contracts.Common;
using CleanArchitecture.SampleProject.Application.Contracts.Persistence;
using CleanArchitecture.SampleProject.Application.Exceptions;
using CleanArchitecture.SampleProject.Domain.Entities;
using Mapster;

namespace CleanArchitecture.SampleProject.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQueryHandler : IQueryHandler<GetEventDetailQuery, EventDetailVm>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Category> _categoryRepository;

        public GetEventDetailQueryHandler(IAsyncRepository<Event> eventRepository, IAsyncRepository<Category> categoryRepository)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<EventDetailVm> Handle(GetEventDetailQuery request, CancellationToken cancellationToken)
        {
            var @event = await _eventRepository.GetByIdAsync(request.Id);

            if (@event is null)
                throw new NotFoundException(nameof(Event), request.Id);

            var eventDetailDto = @event.Adapt<EventDetailVm>();

            var category = await _categoryRepository.GetByIdAsync(@event.CategoryId);

            if (category is null)
                throw new NotFoundException(nameof(Event), request.Id);

            eventDetailDto.Category = new CategoryDto();

            eventDetailDto.Category.Name = category.Name;
            eventDetailDto.Category.Id = category.CategoryId;

            return eventDetailDto;
        }
    }
}
