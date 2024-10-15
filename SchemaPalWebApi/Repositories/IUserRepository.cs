using SchemaPalWebApi.Models;

namespace SchemaPalWebApi.Repositories
{
    public interface IUserRepository
    {
        Guid CreateUser(User user);

        User GetUserByUsername(string username);

        User GetUserById(Guid id);

        bool UserExists(string username);
    }
}
