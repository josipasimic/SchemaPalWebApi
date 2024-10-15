using SchemaPalWebApi.Models;

namespace SchemaPalWebApi.Repositories
{
    public interface IUserRepository
    {
        Guid CreateUser(UserRecord user);

        UserRecord GetUserByUsername(string username);

        UserRecord GetUserById(Guid id);

        bool UserExists(string username);
    }
}
