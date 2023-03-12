using ToDoApp.Models;

namespace ToDoApp.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly List<Todo> _todos = new()
    {
        new Todo()
        {
            Title = "Prepare bUnit presentation", 
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. "
        },
        new Todo()
        {
            Title = "Commit changes", 
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. "
        },
        new Todo()
        {
            Title = "Merge in dev branch", 
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. "
        },
        new Todo()
        {
            Title = "Finish up sprint", 
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. "
        },
        new Todo()
        {
            Title = "Log time", 
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. "
        }
    };

    public TodoRepository()
    {
        _todos[1].MarkDone();
        _todos[2].MarkDone();
    }

    public async Task<IEnumerable<Todo>?> GetTodosAsync() => await Task.Run(() => _todos.ToList());

    public async Task<Todo?> GetTodoAsync(Guid id) => await Task.Run(() => _todos.FirstOrDefault(t => t.Id == id));
}