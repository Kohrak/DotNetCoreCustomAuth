
namespace Auth
{
    public class SampleTokenService : ITokenService
    {
        //Validation logic goes here, since this is just an example I will be using a simple if else statement
        public bool IsTokenValid(string token)
        {
            if (token == "rxZyPugoPIz29hHy")
            {
                return true;
            }

            return false;
        }
    }
}
