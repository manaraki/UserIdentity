using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Application.Interfaces;
using UserIdentity.Application.Services;
using UserIdentity.Infra.Data.Contexts;
using UserIdentity.Infra.Data.Interfaces;
using UserIdentity.Infra.Data.Repositories;

namespace UserIdentity.Infra.Ioc
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddScoped<IAuthenticateUserService, AuthenticateUserService>();
            services.AddScoped<IAddUserService, AddUserService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>(); 
            services.AddScoped<IUnitOfWork, UnitOfWork>();   


        }
        
    }
}
