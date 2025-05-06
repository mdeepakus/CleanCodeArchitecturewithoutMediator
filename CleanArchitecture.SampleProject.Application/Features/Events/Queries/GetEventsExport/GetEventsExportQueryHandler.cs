using CleanArchitecture.SampleProject.Application.Contracts.Common;
using CleanArchitecture.SampleProject.Application.Contracts.Infrastructure;
using CleanArchitecture.SampleProject.Application.Contracts.Persistence;
using CleanArchitecture.SampleProject.Domain.Entities;
using Mapster;

namespace CleanArchitecture.SampleProject.Application.Features.Events.Queries.GetEventsExport
{
    public class GetEventsExportQueryHandler : IQueryHandler<GetEventsExportQuery, EventExportFileVm>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly ICsvExporter _csvExporter;

        public GetEventsExportQueryHandler(IAsyncRepository<Event> eventRepository, ICsvExporter csvExporter)
        {
            _eventRepository = eventRepository;
            _csvExporter = csvExporter;
        }

        public async Task<EventExportFileVm> Handle(GetEventsExportQuery request, CancellationToken cancellationToken)
        {
            var eventList =  (await _eventRepository.ListAllAsync()).OrderBy(x => x.Date);
            var allEvents = eventList.Adapt<List<EventExportDto>>();
            var fileData = _csvExporter.ExportEventsToCsv(allEvents);

            var eventExportFileDto = new EventExportFileVm() { ContentType = "text/csv", Data = fileData, EventExportFileName = $"{Guid.NewGuid()}.csv" };

            return eventExportFileDto;
        }
    }
}
