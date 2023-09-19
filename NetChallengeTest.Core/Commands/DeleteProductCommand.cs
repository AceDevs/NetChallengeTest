using MediatR;

namespace NetChallengeTest.Core.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}