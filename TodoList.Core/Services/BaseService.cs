using TodoList.Core.Contracts;
using TodoList.Core.Models;

namespace TodoList.Core.Services
{
    internal class BaseService<M,R> : IBaseService<R,M> where M : BaseItem where R : IBaseRepository<M>
    {
        #region protected fields

        protected readonly R _repozitory;

        #endregion

        #region Properties

        public bool AutoSave { get; set; } = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public BaseService(R repozitory)
        {
            _repozitory = repozitory;
        }

        #endregion

        #region Public Method

        public virtual async Task<M> AddAsync(M model)
        {
            M added = await _repozitory.AddAsync(model);
            if (AutoSave)
                await _repozitory.SaveAsync();
            return added;
        }

        public virtual M Add(M model)
        {
            M added = _repozitory.Add(model);
            if (AutoSave)
                _repozitory.Save();
            return added;
        }

        public virtual void Update(M model)
        {
            _repozitory.Update(model);
        }

        public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            bool flag = await _repozitory.DeleteAsync(id);
            if (AutoSave)
                _repozitory.Save();

            return flag;
        }

        public virtual bool Delete(Guid id)
        {
            bool flag = _repozitory.Delete(id);
            if (AutoSave)
                _repozitory.Save();

            return flag;
        }

        public virtual async Task<List<M>> GetAllAsync(CancellationToken cancellationToken = default) => await _repozitory.GetAllAsync(cancellationToken: cancellationToken);

        public virtual List<M> GetAll() => _repozitory.GetAll();

        public virtual async Task<M> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) => await _repozitory.GetByIdAsync(id);

        public virtual M GetById(Guid id) => _repozitory.GetById(id);

        public virtual async Task<bool> SaveAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _repozitory.SaveAsync(cancellationToken);
            return true;
        }

        public virtual bool Save()
        {
            _repozitory.Save();
            return true;
        }

        public virtual bool Exist(Guid id) => _repozitory.Exist(id);

        #endregion
    }
}