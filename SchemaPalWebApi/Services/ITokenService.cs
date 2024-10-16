using SchemaPalWebApi.DataTransferObjects;
using System.Security.Claims;

namespace SchemaPalWebApi.Services
{
    public interface ITokenService
    {
        AccessToken GenerateToken(Guid userId);
    }
}
