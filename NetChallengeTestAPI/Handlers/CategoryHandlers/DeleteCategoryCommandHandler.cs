using MediatR;
using NetChallengeTest.Core.Commands;
using NetChallengeTestAPI.Services.Repositories;

namespace NetChallengeTestAPI.Handlers.CategoryHandlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoriesRepository _categoriesRepository;
        public DeleteCategoryCommandHandler(ICategoriesRepository categoriesRepository) 
            => _categoriesRepository = categoriesRepository;

        public Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken) 
            => _categoriesRepository.Delete(request.Id);
    }
}
