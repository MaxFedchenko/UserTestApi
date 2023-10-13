using UserTestApi.Domain.Entities;

namespace UserTestApi.Domain.Repositories
{
    public interface IUserTestRepository : IBaseRepository<UserTestEntity, int>
    {
        Task<UserTestSelect[]> GetByUser(string user);
        Task<UserTestEntity?> Get(string user, int testId);
        void UpdatePoints(int userTestId, int points);
    }
}