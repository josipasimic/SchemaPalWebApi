using SchemaPalWebApi.Models;
using SchemaPalWebApi.SchemaPalDbContext;

namespace SchemaPalWebApi.Repositories
{
    public class SqlUserRepository : IUserRepository
    {
        private readonly SchemaPalContext _context;

        public SqlUserRepository(SchemaPalContext context)
        {
            _context = context;
        }

        public Guid CreateUser(UserRecord user)
        {
            user.Id = Guid.NewGuid();

            _context.Users.Add(user);
            _context.SaveChanges();

            return user.Id;
        }

        public UserRecord GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public UserRecord GetUserById(Guid id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public bool UserExists(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }
    }
}
