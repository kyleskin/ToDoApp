using FluentAssertions;
using Moq;
using ToDoApp.Exceptions;
using ToDoApp.Models;
using ToDoApp.Repositories;
using ToDoApp.Services;

namespace ToDoAppTests.ServicesTests;

public class TodoServiceUnitTests
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

    public TodoServiceUnitTests()
    {
        _todos[1].MarkDone();
        _todos[2].MarkDone();
    }
    
    [Fact]
    public async void GetTodosAsync_ShouldReturnAllTodos_WhenTodosExistInRepo()
    {
        // Arrange
        var mockTodoRepo = new Mock<ITodoRepository>();
        mockTodoRepo.Setup(m => m.GetTodosAsync()).ReturnsAsync(_todos);
        var sut = new TodoService(mockTodoRepo.Object);
        
        // Act
        var todos = await sut.GetTodosAsync();

        // Assert
        mockTodoRepo.Verify(m => m.GetTodosAsync(), Times.Once);
        todos.Should().BeEquivalentTo(_todos);
    }

    [Fact]
    public async void GetTodosAsync_ShouldReturnNull_WhenNoTodosExistInRepo()
    {
        // Arrange
        var mockTodoRepo = new Mock<ITodoRepository>();
        mockTodoRepo.Setup(m => m.GetTodosAsync()).ReturnsAsync((IEnumerable<Todo>)null);
        var sut = new TodoService(mockTodoRepo.Object);
        
        // Act
        var todos = await sut.GetTodosAsync();

        // Assert
        mockTodoRepo.Verify(m => m.GetTodosAsync(), Times.Once);
        todos.Should().BeNull();
    }
    
    [Fact]
    public async void GetInProgressTodosAsync_ShouldReturnOnlyInProgressTodos_WhenInProgressTodosExistInRepo()
    {
        // Arrange
        var mockTodoRepo = new Mock<ITodoRepository>();
        mockTodoRepo.Setup(m => m.GetTodosAsync()).ReturnsAsync(_todos.Where(t => t.InProgress));
        var sut = new TodoService(mockTodoRepo.Object);
        
        // Act
        var todos = await sut.GetInProgressTodosAsync();

        // Assert
        mockTodoRepo.Verify(m => m.GetTodosAsync(), Times.Once);
        todos.Should().NotContain(t => !t.InProgress);
    }
    
    [Fact]
    public async void GetInProgressTodosAsync_ShouldReturnNull_WhenNoInProgressTodosExistInRepo()
    {
        // Arrange
        var mockTodoRepo = new Mock<ITodoRepository>();
        mockTodoRepo.Setup(m => m.GetTodosAsync()).ReturnsAsync((IEnumerable<Todo>)null);
        var sut = new TodoService(mockTodoRepo.Object);
        
        // Act
        var todos = await sut.GetInProgressTodosAsync();

        // Assert
        mockTodoRepo.Verify(m => m.GetTodosAsync(), Times.Once);
        todos.Should().BeNull();
    }

    [Fact]
    public async void GetTodo_ShouldReturnTodo_WhenCalledWithValidId()
    {
        // Arrange
        var id = _todos[0].Id;
        var mockTodoRepo = new Mock<ITodoRepository>();
        mockTodoRepo.Setup(m => m.GetTodoAsync(id).Result).Returns(_todos[0]);
        var sut = new TodoService(mockTodoRepo.Object);

        // Act
        var todo = await sut.GetTodoAsync(id);

        // Assert
        mockTodoRepo.Verify(m => m.GetTodoAsync(id), Times.Once);
        todo.Should().BeEquivalentTo(_todos[0]);
    }
    
    [Fact]
    public async void GetTodo_ShouldThrowTodoNotFoundException_WhenCalledWithInvalidId()
    {
        // Arrange
        var mockTodoRepo = new Mock<ITodoRepository>();
        mockTodoRepo.Setup(m => m.GetTodoAsync(It.IsAny<Guid>()).Result).Returns((Todo)null);
        var sut = new TodoService(mockTodoRepo.Object);

        // Act
        Func<Task> action = () => sut.GetTodoAsync(new Guid());

        // Assert
        await action.Should().ThrowAsync<TodoNotFoundException>();
    }
}