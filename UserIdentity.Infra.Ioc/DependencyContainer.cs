using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Application.Interfaces;
using UserIdentity.Application.Services;
using UserIdentity.Domain.Entities.User;
using UserIdentity.Infra.Data.Contexts;
using UserIdentity.Infra.Data.Interfaces;
using UserIdentity.Infra.Data.Repositories;

namespace UserIdentity.Infra.Ioc
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddScoped<IAuthenticateUserService, AuthenticateUserService>();
            services.AddScoped<IAddUserService, AddUserService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            #endregion

            #region Repository
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<Role>, GenericRepository<Role>>();
            services.AddScoped<IGenericRepository<UserRole>, GenericRepository<UserRole>>();
            services.AddScoped<IGenericRepository<Permission>, GenericRepository<Permission>>();
            services.AddScoped<IGenericRepository<RolePermission>, GenericRepository<RolePermission>>();
            #endregion

        }

    }
}
