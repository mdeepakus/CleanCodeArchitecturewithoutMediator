using CleanArchitecture.SampleProject.Application.Contracts.Common;
using CleanArchitecture.SampleProject.Application.Contracts.Persistence;
using CleanArchitecture.SampleProject.Domain.Entities;
using Mapster;

namespace CleanArchitecture.SampleProject.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQueryHandler : IQueryHandler<GetCategoriesListQuery, List<CategoryListVm>>
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        public GetCategoriesListQueryHandler(IAsyncRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryListVm>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            var allCategories = (await _categoryRepository.ListAllAsync()).OrderBy(x => x.Name);
            return allCategories.Adapt<List<CategoryListVm>>();
        }



    }
}
