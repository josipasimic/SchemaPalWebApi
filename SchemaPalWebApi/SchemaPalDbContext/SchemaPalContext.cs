using Microsoft.EntityFrameworkCore;
using SchemaPalWebApi.Models;

namespace SchemaPalWebApi.SchemaPalDbContext
{
    public class SchemaPalContext : DbContext
    {
        public SchemaPalContext(DbContextOptions<SchemaPalContext> options) : base(options) { }

        public DbSet<DatabaseSchemaRecord> DatabaseSchemaRecords { get; set; }

        public DbSet<UserRecord> Users { get; set; }
    }
}