using SchemaPalWebApi.Models;
using System.Collections.Concurrent;

namespace SchemaPalWebApi.Repositories
{
    public interface IDatabaseSchemaRepository
    {
        void AddSchema(DatabaseSchemaRecord schema);

        IEnumerable<DatabaseSchemaRecord> GetSchemasByUserId(Guid userId);

        DatabaseSchemaRecord GetSchemaById(Guid id);

        void UpdateSchema(DatabaseSchemaRecord schema);

        void DeleteSchema(Guid id);
    }
}

