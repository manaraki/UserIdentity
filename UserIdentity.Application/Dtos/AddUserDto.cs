using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Domain.Entities.User;

namespace UserIdentity.Application.Dtos
{
    public class AddUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public virtual List<RoleDto> Roles { get; set; }
    }
}
