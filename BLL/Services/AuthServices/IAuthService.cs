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
        string Authenticate(string username, string password);
        string GenerateJwtToken( string Role, int Id);
        string Register(PersonDto user);

    }
}
