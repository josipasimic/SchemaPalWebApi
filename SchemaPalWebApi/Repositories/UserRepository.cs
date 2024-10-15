using SchemaPalWebApi.Models;
using System.Collections.Concurrent;

namespace SchemaPalWebApi.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly ConcurrentDictionary<Guid, User> _usersById = new();
        private readonly ConcurrentDictionary<string, User> _usersByUsername = new();

        public Guid CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            _usersById[user.Id] = user;
            _usersByUsername[user.Username] = user;

            return user.Id;
        }

        public User GetUserByUsername(string username)
        {
            _usersByUsername.TryGetValue(username, out var user);

            return user;
        }

        public User GetUserById(Guid id)
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
