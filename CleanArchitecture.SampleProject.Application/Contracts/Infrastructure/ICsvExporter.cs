using CleanArchitecture.SampleProject.Application.Features.Events.Queries.GetEventsExport;

namespace CleanArchitecture.SampleProject.Application.Contracts.Infrastructure
{
    public interface ICsvExporter
    {
        byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos);
    }
}
