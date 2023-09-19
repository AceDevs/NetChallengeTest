using MediatR;
using NetChallengeTest.Core.Commands;
using NetChallengeTestAPI.Services.Repositories;

namespace NetChallengeTestAPI.Handlers.ProductHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IProductsRepository _productsRepository;
        public DeleteProductCommandHandler(IProductsRepository productsRepository) 
            => _productsRepository = productsRepository;

        public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken) 
            => _productsRepository.Delete(request.Id);
    }
}
