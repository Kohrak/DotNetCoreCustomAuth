
namespace Auth.TokenService
{
    public interface ITokenService
    {
        bool IsTokenValid(string token);
    }
}
