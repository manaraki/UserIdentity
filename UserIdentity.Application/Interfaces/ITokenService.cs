using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Application.Dtos;
using UserIdentity.Domain.Entities.User;

namespace UserIdentity.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user,TokenDto token);
        UserFromTokenDto GetUserFromToken(string token);       
    }
}
