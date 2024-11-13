namespace TodoList.Core.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        List<T> GetAll();
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        T GetById(Guid id);
        bool Exist(Guid id);
        Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
        bool Delete(Guid id);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        T Add(T entity);
        void Update(T entity);
        Task RefteshDataAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
        Task SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Save();
    }
}