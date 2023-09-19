using MediatR;
using NetChallengeTest.Core.Models;

namespace NetChallengeTest.Core.Commands
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
    }
}