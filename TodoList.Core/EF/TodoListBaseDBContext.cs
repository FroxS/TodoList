using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;
using TodoList.Core.Models;

namespace TodoList.Core.EF
{
    /// Add-Migration "Init database" -context TodoListBaseDBContext
    /// Update-Database -context TodoListBaseDBContext
    /// <summary>
    /// 
    /// </summary>
    public class TodoListBaseDBContext : DbContext
    {
        #region Public properties

        public string DatabasePath { get; }

        public bool MokDatabase { get; set; } = false;

        #endregion

        #region Tables

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskISubtem> SubTasks { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public TodoListBaseDBContext()
        {
            DatabasePath = "TodoList.sqlite";
        }

        public TodoListBaseDBContext(DbContextOptions options) :base(options)
        {
            //DatabasePath = "TodoList.sqlite";
        }

        #endregion

        #region Configurtaion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TaskItem>()
                .HasMany(x => x.Items)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.IdParent)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
        {
            if(MokDatabase)
            {
                base.OnConfiguring(options);
                return;
            }
            if(!options.IsConfigured)
                options.UseSqlite($"Data Source={DatabasePath}");
        }

        #endregion
    }
}