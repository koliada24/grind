using MediatR;
using MediatrDemo1.Data;

namespace MediatrDemo1.Handlers
{
    public record GetAllToDosRequest();

    public record GetAllToDosQuery() : IRequest<GetAllToDosResult>;

    public record GetAllToDosResult(IEnumerable<ToDoTask> Tasks);

    public record GetAllToDosResponse(IEnumerable<ToDoTask> Tasks);

    public class GetAllToDosQueryHandler(IToDoRepository repository) : IRequestHandler<GetAllToDosQuery, GetAllToDosResult>
    {
        public async Task<GetAllToDosResult> Handle(GetAllToDosQuery request, CancellationToken cancellationToken)
        {
            var todos = repository.GetAll();

            var result = new GetAllToDosResult(todos);

            return result;
        }
    }
}
