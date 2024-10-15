using SchemaPalWebApi.Models;
using System.Collections.Concurrent;

namespace SchemaPalWebApi.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<Guid, UserRecord> _usersById = new();
        private readonly ConcurrentDictionary<string, UserRecord> _usersByUsername = new();

        public Guid CreateUser(UserRecord user)
        {
            user.Id = Guid.NewGuid();
            _usersById[user.Id] = user;
            _usersByUsername[user.Username] = user;

            return user.Id;
        }

        public UserRecord GetUserByUsername(string username)
        {
            _usersByUsername.TryGetValue(username, out var user);

            return user;
        }

        public UserRecord GetUserById(Guid id)
        {
            _usersById.TryGetValue(id, out var user);

            return user;
        }

        public bool UserExists(string username)
        {
            var exists = _usersByUsername.ContainsKey(username);

            return exists;
        }
    }
}
