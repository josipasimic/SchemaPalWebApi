namespace SchemaPalWebApi.Models
{
    public class UserRecord
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }
    }
}
