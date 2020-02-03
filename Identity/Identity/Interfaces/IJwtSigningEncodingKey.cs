using Microsoft.IdentityModel.Tokens;

namespace Identity.Interfaces
{
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }
        SecurityKey GetKey();

    }
}
