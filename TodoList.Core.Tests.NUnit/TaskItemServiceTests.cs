using NUnit.Framework;
using TodoList.Core.EF;
using TodoList.Core.Models;
using TodoList.Core.Repository;
using TodoList.Core.Services;

namespace TodoList.Core.Tests.NUnit
{
    [TestFixture]
    public class TaskItemServiceTests
    {
        #region Fields

        private TodoListBaseDBContext _context;
        private TaskItemService _service;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TaskItemServiceTests()
        {
            _context = DbContextMocker.GetTodoListDbContext(nameof(TaskItemServiceTests));
            var repository = new TaskItemRepository(_context);
            _service = new TaskItemService(repository);
        }

        #endregion

        #region Tests

        [TearDown]
        public void TearDown()
        {
            
        }

        [Test]
        public void GetTasksForToday_ShouldReturnTasksForToday()
        {
            _service.Add(new TaskItem { Title = "Task Today", DueDate = DateTime.Today, AddNotification = true, IsCompleted = false });
            _service.Add(new TaskItem { Title = "Task Tomorrow", DueDate = DateTime.Today.AddDays(1), AddNotification = true, IsCompleted = true });
            _service.Save();
            var result = _service.GetTasksForToday();
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Task Today", result[0].Title);
            _service.Add(new TaskItem { Title = "Task Today 2", DueDate = DateTime.Today, AddNotification = true, IsCompleted = true });
            result = _service.GetTasksForToday();
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public async Task AddAsync_ShouldAddNewTaskItem()
        {
            var task = new TaskItem { Title = "Async Add Task", DueDate = DateTime.Today };
            var result = await _service.AddAsync(task);
            Assert.NotNull(result);
            Assert.AreEqual("Async Add Task", result.Title);

            bool flag = await _service.DeleteAsync(task.Id);
            Assert.IsTrue(flag);
        }

        [Test]
        public void AddAsync_ShouldCheckIsTaskItemToday()
        {
            var task = new TaskItem {DueDate = DateTime.Today };
            Assert.IsTrue(task.isToday());
        }

        #endregion
    }
}