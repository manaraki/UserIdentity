using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Domain.Entities.User;


namespace UserIdentity.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = "Data Source=.;Initial Catalog=UserIdentityCopy;Integrated Security=true;Trust Server Certificate=true;";
            optionsBuilder.UseSqlServer(connection);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User
            modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();
            #endregion

            #region Role
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, RoleTiltle = "Admin" });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, RoleTiltle = "Product Manager" });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 3, RoleTiltle = "Operator" });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 4, RoleTiltle = "Customer" });
            #endregion

            #region Permission

            modelBuilder.Entity<Permission>().HasData(new Permission { Id = 1, PermissionTitle = "AddUser"});
            modelBuilder.Entity<Permission>().HasData(new Permission { Id = 2, PermissionTitle = "ViewUsers"});
            modelBuilder.Entity<Permission>().HasData(new Permission { Id = 3, PermissionTitle = "AddProduct"});
            modelBuilder.Entity<Permission>().HasData(new Permission { Id = 4, PermissionTitle = "ViewProducts"});
            #endregion

            #region RolePermission
            modelBuilder.Entity<RolePermission>().HasData(new RolePermission { Id = 1, PermissionId = 1, RoleId = 1 });
            modelBuilder.Entity<RolePermission>().HasData(new RolePermission { Id = 2, PermissionId = 3, RoleId = 2 });
            modelBuilder.Entity<RolePermission>().HasData(new RolePermission { Id = 3, PermissionId = 4, RoleId = 2 });
            modelBuilder.Entity<RolePermission>().HasData(new RolePermission { Id = 4, PermissionId = 2, RoleId = 3 });
            modelBuilder.Entity<RolePermission>().HasData(new RolePermission { Id = 5, PermissionId = 4, RoleId = 3 });
            #endregion

            #region
            modelBuilder.Entity<UserRole>().HasData(new UserRole { Id = 1, UserId = 2, RoleId = 1 });
            #endregion
        }
    }
    public class UserIdentityDbContextFactory : IDesignTimeDbContextFactory<DataContext>
    {        
        public UserIdentityDbContextFactory()
        {
            
        }
        

        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            var connection = "Data Source=.;Initial Catalog=UserIdentityCopy;Integrated Security=true;Trust Server Certificate=true;";
            optionsBuilder.UseSqlServer(connection);
            

            return new DataContext(optionsBuilder.Options);
        }
    }
}
