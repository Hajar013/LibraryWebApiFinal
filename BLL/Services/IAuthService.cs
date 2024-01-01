using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IAuthService<T>
    {
        T Authenticate(string username, string password);
        string GenerateJwtToken(T user);
        void Register(T user);
    }
}
