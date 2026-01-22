using MediatR;
using MediatrDemo1.Data;

namespace MediatrDemo1.Handlers
{
    public record DeleteToDoRequest(Guid Id);

    public record DeleteToDoCommand(Guid Id) : IRequest<DeleteToDoResult>;

    public record DeleteToDoResult();

    public record DeleteToDoResponse();

    public class DeleteToDoCommandHandler(IToDoRepository repository) : IRequestHandler<DeleteToDoCommand, DeleteToDoResult>
    {
        public async Task<DeleteToDoResult> Handle(DeleteToDoCommand command, CancellationToken cancellationToken)
        {
            repository.Remove(command.Id);

            return new DeleteToDoResult();
        }
    }
}
