using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTestApi.Domain.EF;
using UserTestApi.Domain.Entities;

namespace UserTestApi.Domain.Repositories
{
    public class BaseRepository<T, K> : IBaseRepository<T, K> where T : BaseEntity<K>
    {
        protected readonly UserTestsContext Context;

        public BaseRepository(UserTestsContext context)
        {
            Context = context;
        }

        public async Task<T?> Get(K id)
        {
            return await Context.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id!.Equals(id));
        }
        public async Task<T[]> GetAll()
        {
            return await Context.Set<T>().AsNoTracking().ToArrayAsync();
        }
        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }
        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }
        public void Remove(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Context.Set<T>().Remove(entity);
        }

        public async Task SaveChanges()
        {
            await Context.SaveChangesAsync();
        }
    }
}
