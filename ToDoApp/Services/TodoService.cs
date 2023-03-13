using ToDoApp.Exceptions;
using ToDoApp.Models;
using ToDoApp.Repositories;

namespace ToDoApp.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }
    public async Task<IEnumerable<Todo>?> GetTodosAsync() => await _todoRepository.GetTodosAsync();

    public async Task<IEnumerable<Todo>?> GetInProgressTodosAsync()
    {
        var todos = await _todoRepository.GetTodosAsync();
        return todos?.Where(t => t.InProgress) ?? null;
    }

    public async Task<Todo?> GetTodoAsync(Guid id)
    {
        var todo = await _todoRepository.GetTodoAsync(id);
        return todo ?? throw new TodoNotFoundException("Invalid Todo ID.");
    }
}