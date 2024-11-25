using SchemaPalWebApi.Models;
using SchemaPalWebApi.Repositories;

namespace SchemaPalWebApi.Services
{
    public class DataSeederService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public DataSeederService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            var databaseSchemaRepository = scope.ServiceProvider.GetRequiredService<IDatabaseSchemaRepository>();
            var passwordService = scope.ServiceProvider.GetRequiredService<IPasswordService>();

            await SeedDataAsync(userRepository, databaseSchemaRepository, passwordService);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private async Task SeedDataAsync(IUserRepository userRepository,
            IDatabaseSchemaRepository databaseSchemaRepository,
            IPasswordService passwordService)
        {
            var doesUserExist = userRepository.UserExists("demo.korisnik");
            if (doesUserExist)
            {
                return;
            }

            var defaultUser = new UserRecord
            {
                Username = "demo.korisnik",
                PasswordHash = passwordService.HashPassword("demo123!")
            };

            var seedUserId = userRepository.CreateUser(defaultUser);

            var firstDemoSchemaLocation = Path.Combine(AppContext.BaseDirectory, "DemoSchemas", "demoschema1.json");
            var firstDemoSchema = new DatabaseSchemaRecord
            {
                UserId = seedUserId,
                Name = "Moj Spotify",
                SchemaJsonFormat = File.ReadAllText(firstDemoSchemaLocation)
            };

            databaseSchemaRepository.AddSchema(firstDemoSchema);

            var secondDemoSchemaLocation = Path.Combine(AppContext.BaseDirectory, "DemoSchemas", "demoschema2.json");
            var secondDemoSchema = new DatabaseSchemaRecord
            {
                UserId = seedUserId,
                Name = "Web shop skica",
                SchemaJsonFormat = File.ReadAllText(secondDemoSchemaLocation)
            };

            databaseSchemaRepository.AddSchema(secondDemoSchema);
        }
    }
}
