using ToDoApp.Models;

namespace ToDoApp.Repositories;

public interface ITodoRepository
{
    Task<IEnumerable<Todo>?> GetTodosAsync();
    Task<Todo?> GetTodoAsync(Guid id);
}