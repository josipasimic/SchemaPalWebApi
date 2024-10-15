using System.Security.Claims;

namespace SchemaPalWebApi.Services
{
    public interface ITokenService
    {
        bool CheckIfTokenExpired(string token);

        string GenerateToken(Guid userId);

        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
