using MediatR;
using NetChallengeTest.Core.Models;

namespace NetChallengeTest.Core.Commands
{
    public class CreateProductCommand : IRequest<Product>
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
    }
}