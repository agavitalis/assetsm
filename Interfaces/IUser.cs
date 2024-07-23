using AssetsM.Models;

namespace AssetsM.Interfaces
{
    public interface IUser
    {
        Task<User> GetUser(string userId);
        Task<IEnumerable<User>> GetUsers();
    }
}
