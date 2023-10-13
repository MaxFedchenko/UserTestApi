using UserTestApi.Domain.Entities;

namespace UserTestApi.Domain.Repositories
{
    public interface IUserRepository : IBaseRepository<UserEntity, int>
    {
        Task<bool> Exists(string name);
    }
}