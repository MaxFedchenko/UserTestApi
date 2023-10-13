namespace UserTestApi.Business.Services
{
    public interface ITestService
    {
        Task<int> CompleteTest(string user, int testId, Dictionary<int, int> answers);
        Task<Test> GetTest(int testId);
        Task<UserTest[]> GetUserTests(string user);
    }
}