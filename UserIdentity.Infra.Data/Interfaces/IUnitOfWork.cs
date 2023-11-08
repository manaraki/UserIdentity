using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Domain.Entities.User;
using UserIdentity.Infra.Data.Interfaces;
using UserIdentity.Infra.Data.Repositories;

namespace UserIdentity.Infra.Data.Contexts
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Role> RoleRepository { get; }
        IGenericRepository<UserRole> UserRoleRepository { get; }

        void Save();
        void Dispose();

    }
}
