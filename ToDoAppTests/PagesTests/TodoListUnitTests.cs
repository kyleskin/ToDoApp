using Bunit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ToDoApp.Components;
using ToDoApp.Models;
using ToDoApp.Pages;
using ToDoApp.Services;
using ToDoApp.Components;


namespace ToDoAppTests.PagesTests;

public class TodoListUnitTests
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

    public TodoListUnitTests()
    {
        _todos[1].MarkDone();
        _todos[2].MarkDone();
    }

    [Fact]
    public void Todos_ShouldOnlyContainInProgressTodos_WhenPageLoads()
    {
        // Arrange
        var mockTodoService = new Mock<ITodoService>();
        mockTodoService.Setup(m => m.GetInProgressTodosAsync().Result).Returns(_todos.Where(t => t.InProgress));
        var ctx = new TestContext();
        ctx.Services.AddSingleton(mockTodoService.Object);
        var cut = ctx.RenderComponent<TodoList>();

        // Act
        var todos = cut.Instance.Todos;

        // Assert
        mockTodoService.Verify(m => m.GetInProgressTodosAsync(), Times.Once);
        todos.Should().BeEquivalentTo(_todos.Where(t => t.InProgress));
    }

    [Fact]
    public void Todos_ShouldBeNull_WhenNoInProgressTodosExistOnPageLoad()
    {
        // Arrange
        var mockTodoService = new Mock<ITodoService>();
        mockTodoService.Setup(m => m.GetInProgressTodosAsync().Result).Returns((IEnumerable<Todo>)null);
        var ctx = new TestContext();
        ctx.Services.AddSingleton(mockTodoService.Object);
        var cut = ctx.RenderComponent<TodoList>();

        // Act
        var todos = cut.Instance.Todos;
        
        // Assert
        todos.Should().BeNull();
    }

    [Fact]
    public async void ShowAllTodos_ShouldLoadAllTodos_WhenCalled()
    {
        // Arrange
        var mockTodoService = new Mock<ITodoService>();
        mockTodoService.Setup(m => m.GetTodosAsync().Result).Returns(_todos);
        var ctx = new TestContext();
        ctx.Services.AddSingleton(mockTodoService.Object);
        var cut = ctx.RenderComponent<TodoList>();

        // Act
        await cut.Instance.ShowAllTodos();
        var todos = cut.Instance.Todos;

        // Assert
        mockTodoService.Verify(m => m.GetTodosAsync(), Times.Once);
        todos.Should().BeEquivalentTo(_todos);
    }
    
    [Fact]
    public async void ShowAllTodos_ShouldBeNull_WhenNoTodosExist()
    {
        // Arrange
        var mockTodoService = new Mock<ITodoService>();
        mockTodoService.Setup(m => m.GetTodosAsync().Result).Returns((IEnumerable<Todo>)null);
        var ctx = new TestContext();
        ctx.Services.AddSingleton(mockTodoService.Object);
        var cut = ctx.RenderComponent<TodoList>();

        // Act
        await cut.Instance.ShowAllTodos();
        var todos = cut.Instance.Todos;
        
        // Assert
        todos.Should().BeNull();
    }

    [Fact]
    public void TodoList_ShouldDisplayOnlyInProgressTodos_WhenPageLoads()
    {
        // Arrange
        var mockTodoService = new Mock<ITodoService>();
        mockTodoService.Setup(m => m.GetInProgressTodosAsync().Result).Returns(_todos.Where(t => t.InProgress));
        var ctx = new TestContext();
        ctx.Services.AddSingleton(mockTodoService.Object);
        var cut = ctx.RenderComponent<TodoList>();

        // Act
        var todos = cut.FindComponents<TodoComponent>();

        // Assert
        todos.Should().HaveCount(3);
    }
}