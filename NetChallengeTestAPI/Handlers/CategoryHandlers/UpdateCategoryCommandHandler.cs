using MediatR;
using NetChallengeTest.Core.Commands;
using NetChallengeTest.Core.Models;
using NetChallengeTestAPI.Services.Repositories;

namespace NetChallengeTestAPI.Handlers.CategoryHandlers
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public UpdateCategoryCommandHandler(ICategoriesRepository categoriesRepository)
            => _categoriesRepository = categoriesRepository;

        public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Id = request.Id,
                CategoryCode = request.CategoryCode,
                CategoryName = request.CategoryName,
            };
            var gotUpdated = await _categoriesRepository.Update(category);
            if (!gotUpdated) throw new Exception("Category not found. No modifications aplied.");
            return category;
        }
    }
}
