using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Domain.Entities.User;

namespace UserIdentity.Application.Interfaces
{
    public interface IUserService
    {
        Task<User>? GetUserAsync(object userId);
        Task<Role> GetRoleAsync(object Id);        
        IEnumerable<UserRole>? GetUsersRoles(int userId);
        //IEnumerable<Permission> GetUserPermissions(int roleId);
    }
}
