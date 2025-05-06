using CleanArchitecture.SampleProject.Domain.Entities;

namespace CleanArchitecture.SampleProject.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents);
    }
}
