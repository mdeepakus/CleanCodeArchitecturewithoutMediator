using CleanArchitecture.SampleProject.Application.Contracts.Infrastructure;
using CleanArchitecture.SampleProject.Application.Features.Events.Queries.GetEventsExport;
using CsvHelper;

namespace CleanArchitecture.SampleProject.Infrastructure
{
    public class CsvExporter : ICsvExporter
    {
        public byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, System.Globalization.CultureInfo.CurrentCulture);
                csvWriter.WriteRecords(eventExportDtos);
            }

            return memoryStream.ToArray();
        }
    }
}
