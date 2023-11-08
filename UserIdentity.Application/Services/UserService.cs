using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities.User;
using UserIdentity.Infra.Data.Contexts;

namespace UserIdentity.Application.Services
{
    public class UserService : IUserService
    {
        UnitOfWork db = new UnitOfWork();

        public async Task<User>? GetUserAsync(object Id)
        {            
            return await db.UserRepository.GetByIdAsync(Id);
        }

        public async Task<Role> GetRoleAsync(object Id)
        {
            return await db.RoleRepository.GetByIdAsync(Id);
        }

        public async Task<IEnumerable<UserRole>>? GetUsersRolesAsync(int userId)
        {
            //var roles = db.UserRoleRepository.GetAll(where = p => p.UserId == userId, includes = "Role");
            return await db.UserRoleRepository.GetAllAsync(p => p.UserId == userId, null, "Role");
            //UserRepository.GetAll()
            //.Include(user => user.UserRoles)
            //.ThenInclude(userRole => userRole.Role);
            //var userRoles = _userRoleRepository.GetUserRoleByUserId(userId);
            //var roles = new List<Role>();
            //foreach (var item in userRoles)
            // {
            //
            //  roles.Add(item.Role);
            //}
            
        }

        /*
        public List<Permission> GetUserPermissions(int roleId)
        {
            var rolePermissions = _rolePermissionRepository.GetPermissionByRoleId(roleId);
            var permissions = new List<Permission>();
            foreach (var item in rolePermissions)
            {
                permissions.Add(item.Permission);
            }
            return permissions;
        }
        */
    }
}
