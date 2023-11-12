using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Domain.Entities.Common;

namespace UserIdentity.Domain.Entities.User
{
    public class RolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId {  get; set; }
        public virtual Role Role { get; set; }
        public virtual Permission Permission { get; set; }  

    }
}
