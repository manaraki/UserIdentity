using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return await db.UserRepository.GetAsync(c =>
            c.Username == userAuthenticateDto.Username &&
            c.Password == RegisterUserService.GetPasswordHash(userAuthenticateDto.Password));
        }

    }
}
