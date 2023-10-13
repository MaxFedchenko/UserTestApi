using UserTestApi.Domain.Entities;

namespace UserTestApi.Domain.Repositories
{
    public interface ITestRepository : IBaseRepository<TestEntity, int>
    {

    }
}