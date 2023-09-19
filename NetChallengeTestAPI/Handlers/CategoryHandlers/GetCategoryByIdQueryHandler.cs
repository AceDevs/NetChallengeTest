using MediatR;
using NetChallengeTest.Core.Models;
using NetChallengeTest.Core.Queries;
using NetChallengeTestAPI.Services.Repositories;

namespace NetChallengeTestAPI.Handlers.CategoryHandlers
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public GetCategoryByIdQueryHandler(ICategoriesRepository categoriesRepository)
            => _categoriesRepository = categoriesRepository;

        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken) 
            => await _categoriesRepository.FindByIdAsync(request.Id);
    }
}
