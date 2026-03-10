using NewWebApplication.Models;

namespace NewWebApplication.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);

        void AddUser(User user);

        void Save();
    }
}