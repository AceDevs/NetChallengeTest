using MediatR;
using NetChallengeTest.Core.Models;

namespace NetChallengeTest.Core.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
    }
}