using CleanArchitecture.SampleProject.Domain.Entities;

namespace CleanArchitecture.SampleProject.Application.Contracts.Services
{
    /// <summary>
    /// Interface for creating categories
    /// </summary>
    public interface ICreateCategoryService
    {
        /// <summary>
        /// Create the category for the supplied category request
        /// </summary>
        /// <param name="category">The category request to create category.</param>
        /// <returns>Category created</returns>
        Task<Category> CreateCategory(Category category);

    }
}
