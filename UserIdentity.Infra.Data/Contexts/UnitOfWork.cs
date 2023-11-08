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
    public class UnitOfWork : IUnitOfWork
    {
        DataContext db = new DataContext();
        private IGenericRepository<User> userRepository;
        private IGenericRepository<Role> roleRepository;
        private IGenericRepository<UserRole> userRoleRepository;
        private IGenericRepository<Permission> permissionRepository;
        private IGenericRepository<RolePermission> rolePermissionRepository;

        #region UserRepository
        public IGenericRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new GenericRepository<User>(db);
                }
                return userRepository;
            }
        }
        #endregion

        #region RoleRepository
        public IGenericRepository<Role> RoleRepository
        {
            get
            {
                if (roleRepository == null)
                {
                    roleRepository = new GenericRepository<Role>(db);
                }
                return roleRepository;
            }
        }
        #endregion

        #region UserRoleRepository
        public IGenericRepository<UserRole> UserRoleRepository
        {
            get
            {
                if (userRoleRepository == null)
                {
                    userRoleRepository = new GenericRepository<UserRole>(db);
                }
                return userRoleRepository;
            }
        }
        #endregion

        #region Permission
        public IGenericRepository<Permission> PermissionRepository
        {
            get
            {
                if (permissionRepository == null)
                {
                    permissionRepository = new GenericRepository<Permission>(db);
                }
                return permissionRepository;
            }
        }
        #endregion

        #region RolePermission
        public IGenericRepository<RolePermission> RolePermissionRepository
        {
            get
            {
                if (rolePermissionRepository == null)
                {
                    rolePermissionRepository = new GenericRepository<RolePermission>(db);
                }
                return rolePermissionRepository;
            }
        }
        #endregion

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
