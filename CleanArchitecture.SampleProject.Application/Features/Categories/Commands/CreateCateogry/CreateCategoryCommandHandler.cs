using CleanArchitecture.SampleProject.Application.Contracts.Common;
using CleanArchitecture.SampleProject.Application.Contracts.Services;
using CleanArchitecture.SampleProject.Application.Exceptions;
using CleanArchitecture.SampleProject.Domain.Entities;
using Mapster;
using System.Net;

namespace CleanArchitecture.SampleProject.Application.Features.Categories.Commands.CreateCateogry
{
    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
    {
        private readonly ICreateCategoryService _createCategoryService;

        public CreateCategoryCommandHandler(ICreateCategoryService createCategoryService)
        {
            _createCategoryService = createCategoryService ?? throw new ApiException($"Value cannot be null for the argument {nameof(createCategoryService)}.", (int)HttpStatusCode.InternalServerError);
        }

        public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var createCategoryCommandResponse = new CreateCategoryCommandResponse();

            var validator = new CreateCategoryCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                createCategoryCommandResponse.Success = false;
                createCategoryCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    createCategoryCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }
            if (createCategoryCommandResponse.Success)
            {
                var category = new Category() { Name = request.Name };
                category = await _createCategoryService.CreateCategory(category);

                createCategoryCommandResponse.Category = category.Adapt<CreateCategoryDto>();
            }

            return createCategoryCommandResponse;
        }
    }
}
