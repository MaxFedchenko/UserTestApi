using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserTestApi.Domain.Entities;
using UserTestApi.Domain.Repositories;

namespace UserTestApi.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Exists(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return await _repository.Exists(name);
        }
        public async Task CreateNewOne(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            _repository.Add(new UserEntity { Name = name });
            await _repository.SaveChanges();
        }
    }
}
