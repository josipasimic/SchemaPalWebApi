using SchemaPalWebApi.Models;

namespace SchemaPalWebApi.DataTransferObjects
{
    public class ExtendedSchemaRecord
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string SchemaJsonFormat { get; set; }

        public DateTime LastSaved { get; set; }

        public ExtendedSchemaRecord(DatabaseSchemaRecord schemaRecord)
        {
            Id = schemaRecord.Id;
            UserId = schemaRecord.UserId;
            Name = schemaRecord.Name;
            SchemaJsonFormat = schemaRecord.SchemaJsonFormat;
            LastSaved = schemaRecord.LastSaved;
        }
    }
}
