using Microsoft.IdentityModel.Tokens;

namespace Identity.Interfaces
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}
