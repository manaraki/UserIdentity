using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    public class RegisterUserService : IRegisterUserService
    {
        UnitOfWork db = new UnitOfWork();
        public async Task<int> Execute(UserRegisterDto userRegisterDto)
        {
            User user = new User()
            {
                Username = userRegisterDto.Username,           
                Password = HashPassword.GetPasswordHash(userRegisterDto.Password)
            };
            await db.UserRepository.InsertAsync(user);
            db.Save();

            return user.Id;
            
        }
        
    }
}
