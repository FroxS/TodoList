using Microsoft.EntityFrameworkCore;
using TodoList.Core.EF;

namespace TodoList.Core.Tests.NUnit
{
    public static class DbContextMocker
    {
        public static TodoListBaseDBContext GetTodoListDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<TodoListBaseDBContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var dbContext = new TodoListBaseDBContext(options) { MokDatabase = true };
            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}