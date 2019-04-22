
namespace Auth
{
    public interface ITokenService
    {
        bool IsTokenValid(string token);
    }
}
