using UserTestApi.Domain.Entities;

namespace UserTestApi.Domain.Repositories
{
    public interface IBaseRepository<T, K> where T : BaseEntity<K>
    {
        void Add(T entity);
        Task<T?> Get(K id);
        Task<T[]> GetAll();
        void Remove(T entity);
        void Update(T entity);
        Task SaveChanges();
    }
}