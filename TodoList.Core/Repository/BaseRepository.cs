#nullable enable
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using TodoList.Core.Contracts;
using TodoList.Core.EF;
using TodoList.Core.Models;
using System.Threading;

namespace TodoList.Core.Repository
{
    internal abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseItem
    {
        #region Properteies

        /// <summary>
        /// Context of database
        /// </summary>
        public TodoListBaseDBContext DbContext { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseRepository(TodoListBaseDBContext dbContext)
        {
            DbContext = dbContext;
        }

        #endregion

        #region Public methods

        public async virtual Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<T> items = DbContext.Set<T>();
            return await items.ToListAsync(cancellationToken);
        }

        public virtual List<T> GetAll()
        {
            IQueryable<T> items = DbContext.Set<T>();
            return items.ToList();
        }

        public async virtual Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var trackedItem = DbContext.ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity is T && e.Entity != null && ((T)e.Entity).Id == id);
            T? item;
            if (trackedItem != null)
            {
                item = trackedItem.Entity;
            }
            else
            {
                if (!(await ExistAsync(id)))
                    return null;
                item = await DbContext.Set<T>().FindAsync(id, cancellationToken);
            }
            return item;
        }

        public virtual T GetById(Guid id)
        {
            var trackedItem = DbContext.ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity is T && e.Entity != null && ((T)e.Entity).Id == id);
            T? item;
            if (trackedItem != null)
            {
                item = trackedItem.Entity;
            }
            else
            {
                if (!(Exist(id)))
                    return null;
                item = DbContext.Set<T>().Find(id);
            }
            return item;
        }

        public virtual bool Exist(Guid id) => DbContext.Set<T>().Any(x => x.Id == id);
        public virtual async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken)) => await DbContext.Set<T>().AnyAsync(x => x.Id == id, cancellationToken);
        public async virtual Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            T? item = await GetByIdAsync(id, cancellationToken);
            DbContext.Set<T>().Remove(item);
            return true;
        }

        public virtual bool Delete(Guid id)
        {
            T? item = DbContext.Set<T>().Find(id);
            if (item == null)
                return false;
            DbContext.Set<T>().Remove(item);
            return true;
        }

        public async virtual Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            DbSet<T> table = DbContext.Set<T>();
            var res = await table.AddAsync(entity, cancellationToken);
            return res.Entity;
        }

        public virtual T Add(T entity)
        {
            DbSet<T> table = DbContext.Set<T>();
            var res = table.Add(entity);
            return res.Entity;
        }

        public virtual void Update(T entity)
        {
            DbContext.Set<T>().Update(entity);
        }

        public virtual async Task RefteshDataAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await DbContext.Entry<T>(entity).ReloadAsync(cancellationToken);
        }

        public async virtual Task SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual void Save()
        {
            DbContext.SaveChanges();
        }

        #endregion

    }
}