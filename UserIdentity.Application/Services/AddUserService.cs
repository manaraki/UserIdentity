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
    public class AddUserService : IAddUserService
    {
        UnitOfWork db = new UnitOfWork();
        
        public async Task<int> Execute(AddUserDto addUserDto)
        {
           
            User user=new User()
            {
                Username = addUserDto.Username,
                Password = PasswordHelper.HashPassword(addUserDto.Password)                
            };
            List<UserRole> userRoles = new List<UserRole>();
            foreach (var item in addUserDto.Roles)
            {
                userRoles.Add(new UserRole()
                {
                    RoleId = item.RoleId,
                    Role = await db.RoleRepository.GetByIdAsync(item.RoleId),
                    User=user,
                    UserId=user.Id
                });
            }
            user.UserRoles = userRoles;
            db.UserRepository.InsertAsync(user);
            db.Save();            

            return user.Id;
        }
    }
}
