using SchemaPalWebApi.Models;

namespace SchemaPalWebApi.DataTransferObjects
{
    public class ShortSchemaRecord
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime LastSaved { get; set; }

        public ShortSchemaRecord(DatabaseSchemaRecord schemaRecord)
        {
            Id = schemaRecord.Id;
            Name = schemaRecord.Name;
            LastSaved = schemaRecord.LastSaved;
        }
    }
}
