using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchemaPalWebApi.DataTransferObjects;
using SchemaPalWebApi.Models;
using SchemaPalWebApi.Repositories;
using System.Security.Claims;

namespace SchemaPalWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseSchemasController : ControllerBase
    {
        private readonly IDatabaseSchemaRepository _repository;

        public DatabaseSchemasController(IDatabaseSchemaRepository repository)
        {
            _repository = repository;
        }

        private Guid GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("Korisnik nije autenticiran.");
            }

            return userId;
        }

        [HttpPost]
        public IActionResult SaveOrUpdateSchema([FromBody] ExtendedSchemaRecord schema)
        {
            var userId = GetUserIdFromClaims();

            var existingSchema = _repository.GetSchemaById(schema.Id);

            if (existingSchema is null)
            {
                var newSchema = new DatabaseSchemaRecord
                {
                    Name = schema.Name,
                    UserId = userId,
                    SchemaJsonFormat = schema.SchemaJsonFormat
                };

                _repository.AddSchema(newSchema);
                return CreatedAtAction(nameof(GetSchema), new { id = newSchema.Id }, newSchema);
            }

            if (existingSchema.UserId != userId)
            {
                return Unauthorized();
            }

            existingSchema.Name = schema.Name;
            existingSchema.SchemaJsonFormat = schema.SchemaJsonFormat;

            _repository.UpdateSchema(existingSchema);
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetSchemasForUser()
        {
            var userId = GetUserIdFromClaims();
            var schemas = _repository.GetSchemasByUserId(userId);

            var shortSchemaRecords = schemas
                .Select(schema => new ShortSchemaRecord(schema))
                .ToList();

            return Ok(shortSchemaRecords);
        }

        [HttpGet("{id}")]
        public IActionResult GetSchema(Guid id)
        {
            var userId = GetUserIdFromClaims();
            var schema = _repository.GetSchemaById(id);
            if (schema == null || schema.UserId != userId)
            {
                return NotFound();
            }

            var extendedSchemaRecord = new ExtendedSchemaRecord(schema);
            return Ok(extendedSchemaRecord);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSchema(Guid id)
        {
            var userId = GetUserIdFromClaims();
            var schema = _repository.GetSchemaById(id);
            if (schema == null || schema.UserId != userId)
            {
                return NotFound();
            }

            _repository.DeleteSchema(id);
            return NoContent();
        }
    }
}