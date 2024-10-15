using System.Security.Claims;

namespace SchemaPalWebApi.Services
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
