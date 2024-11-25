using SchemaPalWebApi.Models;
using SchemaPalWebApi.SchemaPalDbContext;

namespace SchemaPalWebApi.Repositories
{
    public class SqlDatabaseSchemaRepository : IDatabaseSchemaRepository
    {
        private readonly SchemaPalContext _context;

        public SqlDatabaseSchemaRepository(SchemaPalContext context)
        {
            _context = context;
        }

        public void AddSchema(DatabaseSchemaRecord schema)
        {
            schema.Id = Guid.NewGuid();
            schema.LastSaved = DateTime.UtcNow;

            _context.DatabaseSchemaRecords.Add(schema);
            _context.SaveChanges();
        }

        public IEnumerable<DatabaseSchemaRecord> GetSchemasByUserId(Guid userId)
        {
            return _context.DatabaseSchemaRecords.Where(s => s.UserId == userId).ToList();
        }

        public DatabaseSchemaRecord GetSchemaById(Guid id)
        {
            return _context.DatabaseSchemaRecords.FirstOrDefault(s => s.Id == id);
        }

        public void UpdateSchema(DatabaseSchemaRecord schema)
        {
            schema.LastSaved = DateTime.UtcNow;

            _context.DatabaseSchemaRecords.Update(schema);
            _context.SaveChanges();
        }

        public void DeleteSchema(Guid id)
        {
            var schema = _context.DatabaseSchemaRecords.FirstOrDefault(s => s.Id == id);
            if (schema != null)
            {
                _context.DatabaseSchemaRecords.Remove(schema);
                _context.SaveChanges();
            }
        }
    }
}
