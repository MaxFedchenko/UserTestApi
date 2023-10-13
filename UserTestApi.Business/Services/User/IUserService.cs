namespace UserTestApi.Business.Services
{
    public interface IUserService
    {
        Task CreateNewOne(string name);
        Task<bool> Exists(string name);
    }
}