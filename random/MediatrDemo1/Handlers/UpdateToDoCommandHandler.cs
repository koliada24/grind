using MediatR;
using MediatrDemo1.Data;

namespace MediatrDemo1.Handlers
{
    public record UpdateToDoRequest(string Title);

    public record UpdateToDoResponse();

    public record UpdateToDoCommand(Guid Id, string Title) : IRequest<UpdateToDoResult> ;

    public record UpdateToDoResult();

    public class UpdateToDoCommandHandler(IToDoRepository repository) : IRequestHandler<UpdateToDoCommand, UpdateToDoResult>
    {
        public async Task<UpdateToDoResult> Handle(UpdateToDoCommand command, CancellationToken cancellationToken)
        {
            var taskToUpdate = new ToDoTask
            {
                Id = command.Id,
                Title = command.Title,
            };

            repository.Update(taskToUpdate);

            return new UpdateToDoResult();
        }
    }
}
