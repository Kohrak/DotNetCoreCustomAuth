using System;
using System.Collections.Generic;
using System.Text;

namespace Auth
{
    public interface ITokenService
    {
        bool IsTokenValid(string token);
    }
}
