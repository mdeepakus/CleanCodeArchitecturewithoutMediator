

using CleanArchitecture.SampleProject.Application.Responses;

namespace CleanArchitecture.SampleProject.Application.Features.Categories.Commands.CreateCateogry
{
    public class CreateCategoryCommandResponse : BaseResponse
    {
        public CreateCategoryCommandResponse() : base()
        {

        }

        public CreateCategoryDto Category { get; set; }
    }
}