using TodoList.Core.Models;

namespace TodoList.Core.Contracts
{
    public interface IBaseService<R,M> where M : BaseItem where R : IBaseRepository<M>
    {
        Task<M> AddAsync(M model);
        M Add(M model);
        void Update(M model);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        bool Delete(Guid id);
        Task<List<M>> GetAllAsync(CancellationToken cancellationToken = default);
        List<M> GetAll();
        Task<M> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        M GetById(Guid id);
        Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
        bool Save();

    }
}