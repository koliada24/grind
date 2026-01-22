namespace MediatrDemo1.Data
{
    public class ToDoTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }

    public interface IToDoRepository
    {
        IEnumerable<ToDoTask> GetAll();
        void Add(ToDoTask task);
        void Remove(Guid id);
        ToDoTask? GetById(Guid id);
        void Update(ToDoTask task);
    }

    public class ToDoRepository : IToDoRepository
    {
        private readonly List<ToDoTask> _tasks = new();

        public IEnumerable<ToDoTask> GetAll() => _tasks;

        public void Add(ToDoTask task)
        {
            _tasks.Add(task);
        }

        public void Remove(Guid id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);

            if (task != null)
            {
                _tasks.Remove(task);
            }
        }

        public ToDoTask? GetById(Guid id)
        {
            var matchedTask = _tasks.FirstOrDefault(t => t.Id == id);

            return matchedTask;
        }

        public void Update(ToDoTask task)
        {
            var taskToUpdate = _tasks.FirstOrDefault(t => t.Id == task.Id);

            if (taskToUpdate != null)
            {
                taskToUpdate.Title = task.Title;
            }
        }
    }
}
