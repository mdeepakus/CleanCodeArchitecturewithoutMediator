using CleanArchitecture.SampleProject.Application.Contracts.Common;
using CleanArchitecture.SampleProject.Application.Contracts.Persistence;
using CleanArchitecture.SampleProject.Application.Exceptions;
using Mapster;
using Microsoft.Extensions.Logging;
using System.Net;

namespace CleanArchitecture.SampleProject.Application.Features.Categories.Queries.GetCategoriesListWithEvents
{
    public class GetCategoriesListWithEventsQueryHandler : IQueryHandler<GetCategoriesListWithEventsQuery, List<CategoryEventListVm>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<GetCategoriesListWithEventsQueryHandler> _logger;

        public GetCategoriesListWithEventsQueryHandler(ICategoryRepository categoryRepository, ILogger<GetCategoriesListWithEventsQueryHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<List<CategoryEventListVm>> Handle(GetCategoriesListWithEventsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Call initiated to get all catagories with attached events.");
            try
            {
                
                var list = await _categoryRepository.GetCategoriesWithEvents(request.IncludeHistory);
                return list.Adapt<List<CategoryEventListVm>>();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new ApiException($"Error '{e.Message}' occurred while retrieving catagories", (int)HttpStatusCode.InternalServerError, null, e);
            }

        }
    }
}
