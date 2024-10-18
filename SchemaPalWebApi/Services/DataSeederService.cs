using SchemaPalWebApi.Models;
using SchemaPalWebApi.Repositories;

namespace SchemaPalWebApi.Services
{
    public class DataSeederService : IHostedService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDatabaseSchemaRepository _databaseSchemaRepository;
        private readonly IPasswordService _passwordService;

        public DataSeederService(IUserRepository userRepository,
            IDatabaseSchemaRepository databaseSchemaRepository,
            IPasswordService passwordService)
        {
            _userRepository = userRepository;
            _databaseSchemaRepository = databaseSchemaRepository;
            _passwordService = passwordService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            SeedData();
            return Task.CompletedTask;
        }

        private void SeedData()
        {
            var defaultUser = new UserRecord
            {
                Username = "demo.korisnik",
                PasswordHash = _passwordService.HashPassword("demo123!")
            };

            var seedUserId = _userRepository.CreateUser(defaultUser);

            var firstDemoSchemaLocation = Path.Combine(AppContext.BaseDirectory, "DemoSchemas", "demoschema1.json");
            var firstDemoSchema = new DatabaseSchemaRecord
            {
                UserId = seedUserId,
                Name = "Moj Spotify",
                SchemaJsonFormat = File.ReadAllText(firstDemoSchemaLocation)
            };

            _databaseSchemaRepository.AddSchema(firstDemoSchema);

            var secondDemoSchemaLocation = Path.Combine(AppContext.BaseDirectory, "DemoSchemas", "demoschema2.json");
            var secondDemoSchema = new DatabaseSchemaRecord
            {
                UserId = seedUserId,
                Name = "Web shop skica",
                SchemaJsonFormat = File.ReadAllText(secondDemoSchemaLocation)
            };

            _databaseSchemaRepository.AddSchema(secondDemoSchema);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
