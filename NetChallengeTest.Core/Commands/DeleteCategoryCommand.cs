using MediatR;

namespace NetChallengeTest.Core.Commands;

public class DeleteCategoryCommand : IRequest<bool>
{
    public int Id { get; set; }
}