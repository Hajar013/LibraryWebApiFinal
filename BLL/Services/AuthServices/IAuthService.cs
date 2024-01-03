using BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.AuthServices
{
    public interface IAuthService
    {
        PersonDto Authenticate(string username, string password);
        string GenerateJwtToken(PersonDto person);
        void Register(PersonDto person);
    }
}
