using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Application.Common;
using UserIdentity.Application.Dtos;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities.User;
using UserIdentity.Infra.Data.Contexts;
using UserIdentity.Infra.Data.Interfaces;

namespace UserIdentity.Application.Services
{
    public class AuthenticateUserService : IAuthenticateUserService
    {
        UnitOfWork db = new UnitOfWork();
        public async Task<User> Execute(UserAuthenticateDto userAuthenticateDto)
        {
            var username = userAuthenticateDto.Username;
            var password = HashPassword.GetPasswordHash(userAuthenticateDto.Password);
            return await db.UserRepository.GetAsync(c =>
            c.Username == username);
        }

    }
}
