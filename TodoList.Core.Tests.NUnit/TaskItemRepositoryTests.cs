using NUnit.Framework;
using TodoList.Core.EF;
using TodoList.Core.Models;
using TodoList.Core.Repository;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace TodoList.Core.Tests.NUnit;

[TestFixture]
public class TaskItemRepositoryTests
{
    #region Fields

    private readonly TodoListBaseDBContext _context;
    private readonly TaskItemRepository _repository;

    #endregion

    public TaskItemRepositoryTests()
    {
        _context = DbContextMocker.GetTodoListDbContext(nameof(TaskItemRepositoryTests));
        _repository = new TaskItemRepository(_context);
    }


    [TearDown]
    public void TearDown()
    {
        //_context.Database.EnsureDeleted();
        //_context.Dispose();
    }

    [Test]
    public async Task AddAsync_ShouldAddTaskItem()
    {
        var task = new TaskItem { Title = "Test Task", DueDate = DateTime.Today, Id = Guid.NewGuid() };

        var result = await _repository.AddAsync(task);
        await _context.SaveChangesAsync();

        Assert.NotNull(result);
        Assert.AreEqual("Test Task", result.Title);
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnTaskItem()
    {
        var task = new TaskItem { Title = "GetById Test", DueDate = DateTime.Today,  Id = Guid.NewGuid() };
        _repository.Add(task);
        await _repository.SaveAsync();

        var result = await _repository.GetByIdAsync(task.Id);

        Assert.NotNull(result);
        Assert.AreEqual(task.Id, result.Id);
    }

    [Test]
    public async Task DeleteAsync_ShouldDeleteTaskItem()
    {
        var task = new TaskItem { Title = "Delete Test", DueDate = DateTime.Today, Id = Guid.NewGuid() };
        _repository.Add(task);
        await _repository.SaveAsync();

        var result = await _repository.DeleteAsync(task.Id);
        await _repository.SaveAsync();

        var deletedTask = await _repository.GetByIdAsync(task.Id);

        Assert.IsTrue(result);
        Assert.IsNull(deletedTask);
    }
}
