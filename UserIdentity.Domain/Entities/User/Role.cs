using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Domain.Entities.Common;

namespace UserIdentity.Domain.Entities.User
{
    public class Role:BaseEntity
    {
        public int Id { get; set; }
        public string RoleTiltle { get; set; }
        public virtual List<UserRole> UserRoles { get; set;}
        public virtual List<RolePermission> RolePermissions { get; set;}

    }
}
