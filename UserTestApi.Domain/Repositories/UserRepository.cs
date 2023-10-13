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
    public class UserRepository : BaseRepository<UserEntity, int>, IUserRepository
    {
        public UserRepository(UserTestsContext context) : base(context) { }

        public async Task<bool> Exists(string name)
        {
            return await Context.Users.AnyAsync(u => u.Name == name);
        }
    }
}
