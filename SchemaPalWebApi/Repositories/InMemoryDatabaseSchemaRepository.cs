using SchemaPalWebApi.Models;
using System.Collections.Concurrent;

namespace SchemaPalWebApi.Repositories
{
    public class InMemoryDatabaseSchemaRepository : IDatabaseSchemaRepository
    {
        private readonly ConcurrentDictionary<Guid, DatabaseSchemaRecord> _schemas = new();

        public void AddSchema(DatabaseSchemaRecord schema)
        {
            schema.Id = Guid.NewGuid();
            schema.LastSaved = DateTime.UtcNow;
            _schemas.TryAdd(schema.Id, schema);
        }

        public IEnumerable<DatabaseSchemaRecord> GetSchemasByUserId(Guid userId)
        {
            return _schemas.Values.Where(schema => schema.UserId == userId);
        }

        public DatabaseSchemaRecord GetSchemaById(Guid id)
        {
            _schemas.TryGetValue(id, out var schema);
            return schema;
        }

        public void UpdateSchema(DatabaseSchemaRecord schema)
        {
            schema.LastSaved = DateTime.UtcNow; 
            _schemas[schema.Id] = schema;
        }

        public void DeleteSchema(Guid id)
        {
            _schemas.TryRemove(id, out _);
        }
    }
}
