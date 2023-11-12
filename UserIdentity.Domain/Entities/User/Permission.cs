using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Domain.Entities.Common;

namespace UserIdentity.Domain.Entities.User
{
    public class Permission: BaseEntity
    {
        public int Id { get; set; }
        public string PermissionTitle { get; set; }
        
        public List<RolePermission> RolePermissions { get; set; }
    }
}
