using ToDoApp.Models;

namespace ToDoApp.Services;

public interface ITodoService
{
    Task<IEnumerable<Todo>?> GetTodosAsync();
    Task<IEnumerable<Todo>?> GetInProgressTodosAsync();
    Task<Todo?> GetTodoAsync(Guid id);
}