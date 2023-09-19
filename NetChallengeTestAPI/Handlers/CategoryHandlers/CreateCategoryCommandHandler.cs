using MediatR;
using NetChallengeTest.Core.Commands;
using NetChallengeTest.Core.Models;
using NetChallengeTestAPI.Services.Repositories;

namespace NetChallengeTestAPI.Handlers.CategoryHandlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public CreateCategoryCommandHandler(ICategoriesRepository categoriesRepository) 
            => _categoriesRepository = categoriesRepository;

        public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Category
            {
                CategoryCode = request.CategoryCode,
                CategoryName = request.CategoryName,
                CreationDate = DateTime.Now
            };

            var newId = await _categoriesRepository.Add(newCategory);
            newCategory.Id = newId;
            return newCategory;
        }
    }
}
