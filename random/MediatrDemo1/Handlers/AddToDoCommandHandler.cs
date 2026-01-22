using MediatR;
using MediatrDemo1.Data;

namespace MediatrDemo1.Handlers
{
    public record AddToDoRequest(string Title);

    public record AddToDoResponse(Guid Id);

    public record AddToDoCommand(string Title) : IRequest<AddToDoResult>;

    public record AddToDoResult(Guid Id);

    public class AddToDoCommandHandler(IToDoRepository repository) : IRequestHandler<AddToDoCommand, AddToDoResult>
    {
        public async Task<AddToDoResult> Handle(AddToDoCommand request, CancellationToken cancellationToken)
        {
            var taskToAdd = new ToDoTask
            {
                Id = Guid.NewGuid(),
                Title = request.Title
            };

            repository.Add(taskToAdd);

            return new AddToDoResult(taskToAdd.Id);
        }
    }
}
