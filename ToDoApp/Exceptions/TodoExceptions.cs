namespace ToDoApp.Exceptions;

public class TodoNotFoundException : Exception
{
    public TodoNotFoundException()
        : base() {}

    public TodoNotFoundException(string message)
        : base(message) {}
    
    public TodoNotFoundException(string message, Exception inner)
        : base(message, inner) {}
}