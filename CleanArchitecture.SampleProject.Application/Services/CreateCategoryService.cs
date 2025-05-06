using CleanArchitecture.SampleProject.Application.Contracts.Persistence;
using CleanArchitecture.SampleProject.Application.Contracts.Services;
using CleanArchitecture.SampleProject.Application.Exceptions;
using CleanArchitecture.SampleProject.Domain.Entities;
using System.Net;

namespace CleanArchitecture.SampleProject.Domain.Services
{
    /// <summary>
    /// CreateCategoryService Class
    /// </summary>
    public class CreateCategoryService : ICreateCategoryService
    {
        private readonly IAsyncRepository<Category> _categoryRepository;
        public CreateCategoryService(IAsyncRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ApiException($"Value cannot be null for the argument {nameof(categoryRepository)}.", (int)HttpStatusCode.InternalServerError);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task<Category> CreateCategory(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }
    }
}
